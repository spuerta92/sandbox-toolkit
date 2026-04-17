import * as cdk from 'aws-cdk-lib';
import { SnsStack } from './stacks/sns-stack';

export function Main(app: cdk.App) {
  // Get environment from context or use default
  const environment = app.node.tryGetContext('environment') || 'dev';
  const account = process.env.CDK_DEFAULT_ACCOUNT || process.env.AWS_ACCOUNT_ID;
  const region = process.env.CDK_DEFAULT_REGION || process.env.AWS_DEFAULT_REGION || 'us-east-1';

  // Create stack with environment-specific naming
  const stackName = `sns-sqs-s3-stack-${environment}`;

  const snsStack = new SnsStack(app, stackName, {
    stackName: stackName,
    environment: environment,
    env: {
      account: account,
      region: region,
    },
    description: `SNS, SQS, and S3 infrastructure for ${environment} environment`,
  });
}