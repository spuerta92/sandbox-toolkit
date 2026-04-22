# Sandbox Toolkit Summary
This solution contains multiple projects where I have attempted individual building blocks

## SQL Server Script
- mycompany.sql (sql server generated script)
- mycompany-raw.sql (manually created script + data)

## ADO .NET
- Nuget: Microsoft.Data.SqlClient;
- Connecting to SQL Server database to retrieve data
- Using windows authentication

## SQL Server Authentication
- Nuget: Microsoft.Data.SqlClient;
- Connecting to SQL Server database to retrieve data
- Using SQL authentication
- Need to create user profile (review mycompany-raw.sql)

## DAPPER ORM
- Nuget: Microsoft.Data.SqlClient, Dapper
- Connecting to SQL Server database to retrieve data
- Using windows authentication
- Using dapper orm

## .NET Entity Framework
- Nuget: Microsoft.EntityFrameworkCore

## .NET AWS SDK
- Nuget: AWSSDK.Core, AWSSDK.Logger.Core, AWSSDK.S3, AWSSDK.SimpleNotificationService, AWSSDK.SQS
- Created an SNS topic, SQS queue, subscribed SQS queue to SNS topic, published data to an S3 bucket, and logged to AWS Cloudwatch

## BITBUCKET Build / Deployment
### Deployed using bitbucket Pipelines
- Deploy base static html to AWS S3: https://bitbucket.org/spuertahincapie92/deploy-test/src/master/
- Create AWS Resources (SNS, SQS, and S3) using Cloud Formation Template: https://bitbucket.org/spuertahincapie92/sandbox-aws-cloud-formation/src/main/
- Create AWS Resources (SNS, SQS, and S3) using CDK (Typescript): https://bitbucket.org/spuertahincapie92/sandbox-aws-cdk/src/main/

## GITLAB Build / Deployment
### Deployed using gitlab Pipelines
- Deploy base static html to AWS S3: https://gitlab.com/spuerta92-group/deploy-test
- Create AWS Resources (SNS, SQS, and S3) using Cloud Formation Template: https://gitlab.com/spuerta92-group/sandbox-aws-cloud-formation
- Create AWS Resources (SNS, SQS, and S3) using CDK (Typescript): https://gitlab.com/spuerta92-group/sandbox-aws-cdk

## GITHUB Build / Deployment 
### Deployed using Github Workflow Actions
- Deploy base static html to AWS S3: https://github.com/spuerta92/deploy-test
- Create AWS Resources (SNS, SQS, and S3) using Cloud Formation Template: https://github.com/spuerta92/sandbox-aws-cloud-formation 
- Create AWS Resources (SNS, SQS, and S3) using CDK (Typescript): https://github.com/spuerta92/sandbox-aws-cdk

## .NET Hangfire
- Nuget:

## .NET RabbigMQ
- Nuget:

## .NET Caching
- Nuget:

## .NET REST WebAPIs
- Nuget:

## .NET GraphQL
- Nuget:

## Object Oriented Overview

## Data Structures Overview

## Algorithm Overview

## Encryption Algorithms Overview

## FSharp Overview

## Angular Client POC

## VUE Client POC

## REACT Client POC

### Technology
.NET 8, C#, F#, TypeScript, HTML, CSS, Angular, Vue, Angular, AWS, RabbitMQ, Hangfire, SQL Server, ADO .NET, Dapper


