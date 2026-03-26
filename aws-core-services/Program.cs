using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using toolbox.Enums;
using toolbox.Models;

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
        var policy = new Policy
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

    static async Task<List<Message>> ReceiveMessagesByUrl(IAmazonSQS sqsClient, string queueUrl, int maxMessages)
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
        foreach(var message in messages)
        {

        }

        // generate csv 

        // publish csv to s3 + add lifecycle rules

        // log to cloudwatch once csv is published

        // create / start ec2 (virtual machine)

        // create / start ecs (container)

        // create lambda function
    }
}