## CDK Flow
  1. CDK CLI reads cdk.json

     ↓

  2. CDK CLI executes: "npx ts-node --prefer-ts-exts bin/app.ts"

     ↓

  3. bin/app.ts creates the CDK App instance

     ↓

  4. bin/app.ts calls Main(app) from lib/app.ts

     ↓

  5. lib/app.ts creates all your stacks (SnsStack, etc.)

     ↓

  6. CDK synthesizes CloudFormation templates

     ↓

  7. CDK deploys to AWS


## Common Pattern in CDK Projects

  This is the standard CDK project structure:

  ├── bin/

  │   └── app.ts          ← Entry point (minimal, just creates App)

  ├── lib/

  │   ├── app.ts          ← Application logic (creates stacks)

  │   └── stacks/

  │       └── sns-stack.ts ← Individual stack definitions

  ├── cdk.json            ← CDK configuration

  ├── package.json        ← Node.js dependencies
  
  └── tsconfig.json       ← TypeScript configuration

## Bootstrapping (one-time)
npx cdk bootstrap aws://ACCOUNT-ID/REGION

## CDK synth 
converts TypeScript CDK code into CloudFormation YAML/JSON templates

## Required permissions
Cloud Formation, IAM, ECR (Elastic Container Registry), S3, Systems Manager

CDKToolkit |  0/12 | 4:27:55 PM | REVIEW_IN_PROGRESS   | AWS::CloudFormation::Stack | CDKToolkit User Initiated

CDKToolkit |  0/12 | 4:28:05 PM | CREATE_IN_PROGRESS   | AWS::CloudFormation::Stack | CDKToolkit User Initiated

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | LookupRole 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::ECR::Repository       | ContainerAssetsRepository 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::SSM::Parameter        | CdkBootstrapVersion 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | CloudFormationExecutionRole 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::S3::Bucket            | StagingBucket 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | FilePublishingRole 

CDKToolkit |  0/12 | 4:28:08 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | ImagePublishingRole 

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | ImagePublishingRole Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | FilePublishingRole Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | LookupRole Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | CloudFormationExecutionRole Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::SSM::Parameter        | CdkBootstrapVersion Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::S3::Bucket            | StagingBucket Resource creation Initiated

CDKToolkit |  0/12 | 4:28:09 PM | CREATE_IN_PROGRESS   | AWS::ECR::Repository       | ContainerAssetsRepository Resource creation Initiated

CDKToolkit |  1/12 | 4:28:10 PM | CREATE_COMPLETE      | AWS::SSM::Parameter        | CdkBootstrapVersion 

CDKToolkit |  2/12 | 4:28:10 PM | CREATE_COMPLETE      | AWS::ECR::Repository       | ContainerAssetsRepository 

CDKToolkit |  3/12 | 4:28:24 PM | CREATE_COMPLETE      | AWS::S3::Bucket            | StagingBucket 

CDKToolkit |  3/12 | 4:28:25 PM | CREATE_IN_PROGRESS   | AWS::S3::BucketPolicy      | StagingBucketPolicy 

CDKToolkit |  3/12 | 4:28:26 PM | CREATE_IN_PROGRESS   | AWS::S3::BucketPolicy      | StagingBucketPolicy Resource creation Initiated

CDKToolkit |  4/12 | 4:28:26 PM | CREATE_COMPLETE      | AWS::IAM::Role             | FilePublishingRole 

CDKToolkit |  5/12 | 4:28:27 PM | CREATE_COMPLETE      | AWS::IAM::Role             | ImagePublishingRole 

CDKToolkit |  6/12 | 4:28:27 PM | CREATE_COMPLETE      | AWS::IAM::Role             | LookupRole

CDKToolkit |  7/12 | 4:28:27 PM | CREATE_COMPLETE      | AWS::S3::BucketPolicy      | StagingBucketPolicy

CDKToolkit |  8/12 | 4:28:27 PM | CREATE_COMPLETE      | AWS::IAM::Role             | CloudFormationExecutionRole

CDKToolkit |  8/12 | 4:28:27 PM | CREATE_IN_PROGRESS   | AWS::IAM::Policy           | FilePublishingRoleDefaultPolicy

CDKToolkit |  8/12 | 4:28:28 PM | CREATE_IN_PROGRESS   | AWS::IAM::Policy           | ImagePublishingRoleDefaultPolicy

CDKToolkit |  8/12 | 4:28:28 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | DeploymentActionRole

CDKToolkit |  8/12 | 4:28:28 PM | CREATE_IN_PROGRESS   | AWS::IAM::Policy           | FilePublishingRoleDefaultPolicy Resource creation Initiated

CDKToolkit |  8/12 | 4:28:29 PM | CREATE_IN_PROGRESS   | AWS::IAM::Policy           | ImagePublishingRoleDefaultPolicy Resource creation Initiated

CDKToolkit |  8/12 | 4:28:29 PM | CREATE_IN_PROGRESS   | AWS::IAM::Role             | DeploymentActionRole Resource creation Initiated

CDKToolkit |  9/12 | 4:28:44 PM | CREATE_COMPLETE      | AWS::IAM::Policy           | FilePublishingRoleDefaultPolicy 

CDKToolkit | 10/12 | 4:28:44 PM | CREATE_COMPLETE      | AWS::IAM::Policy           | ImagePublishingRoleDefaultPolicy 

CDKToolkit | 11/12 | 4:28:46 PM | CREATE_COMPLETE      | AWS::IAM::Role             | DeploymentActionRole 

CDKToolkit | 12/12 | 4:28:48 PM | CREATE_COMPLETE      | AWS::CloudFormation::Stack | CDKToolkit 