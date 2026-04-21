import * as cdk from 'aws-cdk-lib';
import * as sns from 'aws-cdk-lib/aws-sns';
import * as sqs from 'aws-cdk-lib/aws-sqs';
import * as sns_subscriptions from 'aws-cdk-lib/aws-sns-subscriptions';
import * as s3 from 'aws-cdk-lib/aws-s3';
import { Construct } from 'constructs';

export interface SnsStackProps extends cdk.StackProps {
  environment: string;
}

export class SnsStack extends cdk.Stack {
  public readonly topic: sns.Topic;
  public readonly queue: sqs.Queue;
  public readonly bucket: s3.Bucket;

  constructor(scope: Construct, id: string, props: SnsStackProps) {
    super(scope, id, props);

    const { environment } = props;

    // SNS Topic
    this.topic = new sns.Topic(this, 'NotificationTopic', {
      topicName: `${environment}-v1-notification-topic`,
      displayName: `${environment} V1 Notification Topic`,
    });

    // SQS Queue
    this.queue = new sqs.Queue(this, 'NotificationQueue', {
      queueName: `${environment}-v1-notification-queue`,
      visibilityTimeout: cdk.Duration.seconds(300),
      retentionPeriod: cdk.Duration.days(14),
      receiveMessageWaitTime: cdk.Duration.seconds(20), // Enable long polling
    });

    // Subscribe SQS Queue to SNS Topic
    this.topic.addSubscription(
      new sns_subscriptions.SqsSubscription(this.queue, {
        rawMessageDelivery: true,
      })
    );

    // S3 Bucket with Lifecycle Policies
    this.bucket = new s3.Bucket(this, 'StorageBucket', {
      bucketName: `${environment}-v1-storage-bucket-${cdk.Stack.of(this).account}`,
      versioned: true,
      encryption: s3.BucketEncryption.S3_MANAGED,
      blockPublicAccess: s3.BlockPublicAccess.BLOCK_ALL,
      lifecycleRules: [
        {
          id: 'TransitionToGlacierAndDelete',
          enabled: true,
          transitions: [
            {
              storageClass: s3.StorageClass.GLACIER,
              transitionAfter: cdk.Duration.days(30),
            },
          ],
          expiration: cdk.Duration.days(90), // 30 days in Standard + 60 days in Glacier
          noncurrentVersionTransitions: [
            {
              storageClass: s3.StorageClass.GLACIER,
              transitionAfter: cdk.Duration.days(30),
            },
          ],
          noncurrentVersionExpiration: cdk.Duration.days(90),
        },
      ],
    });

    // Add tags to all resources
    cdk.Tags.of(this).add('Environment', environment);
    cdk.Tags.of(this).add('ManagedBy', 'CDK');

    // CloudFormation Outputs
    new cdk.CfnOutput(this, 'SNSTopicArn', {
      value: this.topic.topicArn,
      description: 'ARN of the SNS Topic',
      exportName: `${cdk.Stack.of(this).stackName}-SNSTopicArn`,
    });

    new cdk.CfnOutput(this, 'SNSTopicName', {
      value: this.topic.topicName,
      description: 'Name of the SNS Topic',
      exportName: `${cdk.Stack.of(this).stackName}-SNSTopicName`,
    });

    new cdk.CfnOutput(this, 'SQSQueueUrl', {
      value: this.queue.queueUrl,
      description: 'URL of the SQS Queue',
      exportName: `${cdk.Stack.of(this).stackName}-SQSQueueUrl`,
    });

    new cdk.CfnOutput(this, 'SQSQueueArn', {
      value: this.queue.queueArn,
      description: 'ARN of the SQS Queue',
      exportName: `${cdk.Stack.of(this).stackName}-SQSQueueArn`,
    });

    new cdk.CfnOutput(this, 'SQSQueueName', {
      value: this.queue.queueName,
      description: 'Name of the SQS Queue',
      exportName: `${cdk.Stack.of(this).stackName}-SQSQueueName`,
    });

    new cdk.CfnOutput(this, 'S3BucketName', {
      value: this.bucket.bucketName,
      description: 'Name of the S3 Bucket',
      exportName: `${cdk.Stack.of(this).stackName}-S3BucketName`,
    });

    new cdk.CfnOutput(this, 'S3BucketArn', {
      value: this.bucket.bucketArn,
      description: 'ARN of the S3 Bucket',
      exportName: `${cdk.Stack.of(this).stackName}-S3BucketArn`,
    });
  }
}
