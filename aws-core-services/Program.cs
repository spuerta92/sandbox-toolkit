using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.EC2;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService.Util;
using Amazon.SQS;
using Amazon.SQS.Model;
using CsvHelper;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Net.Mime;
using toolbox.Enums;
using toolbox.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

static class Program
{
    public static async Task<string> CreateSNSTopicAsync(IAmazonSimpleNotificationService client, string topicName)
    {
        var request = new CreateTopicRequest
        {
            Name = topicName
        };

        var response = await client.CreateTopicAsync(request);

        return response.TopicArn;
    }

    public static async Task<CreateQueueResponse> CreateQueue(IAmazonSQS client, string queueName)
    {
        var request = new CreateQueueRequest
        {
            QueueName = queueName,
            Attributes = new Dictionary<string, string>
                {
                    { "DelaySeconds", "60" },
                    { "MessageRetentionPeriod", "86400" },
                },
        };

        var response = await client.CreateQueueAsync(request);
        Console.WriteLine($"Created a queue with URL : {response.QueueUrl}");

        return response;
    }

    public static async Task<SendMessageResponse> SendMessage(
        IAmazonSQS client,
        string queueUrl,
        string messageBody,
        Dictionary<string, Amazon.SQS.Model.MessageAttributeValue> messageAttributes)
    {
        var sendMessageRequest = new SendMessageRequest
        {
            DelaySeconds = 10,
            MessageAttributes = messageAttributes,
            MessageBody = messageBody,
            QueueUrl = queueUrl,
        };

        var response = await client.SendMessageAsync(sendMessageRequest);
        Console.WriteLine($"Sent a message with id : {response.MessageId}");

        return response;
    }

    static async Task<string> GetQueueArnAsync(IAmazonSQS sqsClient, string queueUrl)
    {
        var response = await sqsClient.GetQueueAttributesAsync(new GetQueueAttributesRequest
        {
            QueueUrl = queueUrl,
            AttributeNames = new List<string> { "QueueArn" }
        });

        return response.QueueARN;
    }

    static async Task SetQueuePolicyAsync(IAmazonSQS sqsClient, string queueUrl, string queueArn, string topicArn)
    {
        var policy = new toolbox.Models.Policy
        {
            //Version = DateTime.Now.ToString("yyyy-MM-dd"),
            Statement = new Statement
            {
                Sid = "Allow-SNS-SendMessage",
                Effect = "Allow",
                Principal = new Principal
                {
                    Service = "sns.amazonaws.com"
                },
                Action = "sqs:SendMessage",
                Resource = queueArn,
                Condition = new Condition
                {
                    ArnEquals = new Dictionary<string, string>
                    {
                        { "aws:SourceArn", topicArn }
                    }
                }
            }
        };

        await sqsClient.SetQueueAttributesAsync(new SetQueueAttributesRequest
        {
            QueueUrl = queueUrl,
            Attributes = new Dictionary<string, string>
            {
                { "Policy", JsonConvert.SerializeObject(policy, Formatting.Indented)  }
            }
        });
    }

    static async Task<string> SubscribeQueueToTopicAsync(IAmazonSimpleNotificationService snsClient, string topicArn, string queueArn)
    {
        var response = await snsClient.SubscribeAsync(new SubscribeRequest
        {
            TopicArn = topicArn,
            Protocol = "sqs",
            Endpoint = queueArn
        });

        return response.SubscriptionArn;
    }

    static async Task<List<Amazon.SQS.Model.Message>> ReceiveMessagesByUrl(IAmazonSQS sqsClient, string queueUrl, int maxMessages)
    {
        // https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/sqs-short-and-long-polling.html
        var messageResponse = await sqsClient.ReceiveMessageAsync(
            new ReceiveMessageRequest()
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = maxMessages,
                WaitTimeSeconds = 1
            });
        return messageResponse.Messages;
    }

    static async Task<byte[]> CreateCsvFile<T>(IEnumerable<T> data)
    {
        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteHeader<T>();
            await csv.WriteRecordsAsync(data);
            await csv.FlushAsync();
            return memoryStream.ToArray();
        }
    }

    static async Task<bool> CreateBucketWithObjectLock(IAmazonS3 client, string bucketName, bool enableObjectLock)
    {
        try
        {
            var request = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true,
                ObjectLockEnabledForBucket = enableObjectLock,
            };

            var response = await client.PutBucketAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error creating bucket: '{ex.Message}'");
            return false;
        }
    }

    static async Task PublishObjectToS3Async(IAmazonS3 client, string bucketName, string keyName, byte[] fileBytes, string contentType)
    {
        using (var newMemoryStream = new MemoryStream(fileBytes))
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = keyName,
                BucketName = bucketName,
                ContentType = contentType
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);
        }
    }

    static async Task ConfigureCloudWatchLogs(IAmazonCloudWatchLogs client, string logGroupName, string logStreamName)
    {
        // log group
        var logGroup = new CreateLogGroupRequest
        {
            LogGroupName = logGroupName,
        };

        await client.CreateLogGroupAsync(logGroup);

        // log stream
        var logStream = new CreateLogStreamRequest
        {
            LogGroupName = logGroupName,
            LogStreamName = logStreamName,
        };

        await client.CreateLogStreamAsync(logStream);
    }

    /// <summary>
    /// Testing out AWS SDK core services S3, SNS, SQS, EC2, Cloudwatch
    /// </summary>
    /// <returns></returns>
    static async Task Main()
    {
        // create sns topic
        var snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.USEast1);
        var topicArn = await CreateSNSTopicAsync(snsClient, "MyCompanyTopic");

        // create sqs queue
        var sqsClient = new AmazonSQSClient(RegionEndpoint.USEast1);
        var createQueueResponse = await CreateQueue(sqsClient, "MyCompanyQueue");

        var messageAttributes = new Dictionary<string, Amazon.SQS.Model.MessageAttributeValue>
            {
                { "Department",   new Amazon.SQS.Model.MessageAttributeValue { DataType = "String", StringValue = Departments.RiskAndCompliance.ToString() } },
                { "Role",  new Amazon.SQS.Model.MessageAttributeValue { DataType = "String", StringValue = Roles.PortfolioManager.ToString() } },
                { "Project", new Amazon.SQS.Model.MessageAttributeValue { DataType = "String", StringValue = Projects.RegulatoryReportingAutomation.ToString() } },
            };

        // send test message to sqs queue
        var messageBody = $"message: {Guid.NewGuid()}";
        var queueUrl = createQueueResponse.QueueUrl;
        var sendMsgResponse = await SendMessage(sqsClient, queueUrl, messageBody, messageAttributes);

        // subscribe queue to topic
        string queueArn = await GetQueueArnAsync(sqsClient, queueUrl);
        await SetQueuePolicyAsync(sqsClient, queueUrl, queueArn, topicArn);
        string subscriptionArn = await SubscribeQueueToTopicAsync(snsClient, topicArn, queueArn);

        // post message to sns topic
        await snsClient.PublishAsync(new PublishRequest
        {
            TopicArn = topicArn,
            Message = "Hello from SNS"
        });

        // poll messages, retrieve message from queue
        var messages = await ReceiveMessagesByUrl(sqsClient, queueUrl, 10);
        Console.WriteLine(JsonConvert.SerializeObject(messages, Formatting.Indented));

        // store message in the database
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCompany; Integrated Security=true; Trusted_Connection=true;";
        foreach (var message in messages)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new { MessageBody = message.Body };
                await dbConnection.ExecuteAsync("dbo.InsertQueueMessages", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // generate csv 
        IEnumerable<QueueMessage> retrievedMessages = null;
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            retrievedMessages = await dbConnection.QueryAsync<QueueMessage>("dbo.GetQueueMessages", null, commandType: CommandType.StoredProcedure);
        }

        if (retrievedMessages == null)
        {
            Console.WriteLine("No message retrieved from sns queue");
            return;
        }

        var csvBytes = await CreateCsvFile<QueueMessage>(retrievedMessages);

        // publish csv to s3 + add lifecycle rules
        var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
        var bucketName = "my-local-test-company-bucket";
        await CreateBucketWithObjectLock(s3Client, bucketName, false);
        await PublishObjectToS3Async(s3Client, bucketName, "/local/messages/data.csv", csvBytes, "text/csv");

        // log to cloudwatch once csv is published
        var cloudWatchClient = new AmazonCloudWatchLogsClient(RegionEndpoint.USEast1);
        var logGroup = "cloudwatchlogs-mycompany-loggroup";
        var logStream = "cloudwatchlogs-mycompany-logstream";
        await ConfigureCloudWatchLogs(cloudWatchClient, logGroup, logStream);
        var logEvent = new PutLogEventsRequest
        {
            LogGroupName = logGroup,
            LogStreamName = logStream,
            LogEvents = new List<InputLogEvent> 
            { 
                new InputLogEvent
                {
                    Message = "Hello from mycompany! the csv file with the message was published to s3",
                    Timestamp = DateTime.Now
                }
            }
        };
        await cloudWatchClient.PutLogEventsAsync(logEvent);

        // create / start ec2 (virtual machine)
        //var ec2Client = new AmazonEC2Client(RegionEndpoint.USEast1);
        
    }
}