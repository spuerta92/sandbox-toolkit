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

  ---
  Common Pattern in CDK Projects

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