import * as cdk from 'aws-cdk-lib';
import { SnsStack } from './stacks/sns-stack';

export function Main(app: cdk.App) {
  var snsStack = new SnsStack(app, 'SnsStack', {
    env: {
      account: "",
      region: "us-east-1",
    },
  });
}