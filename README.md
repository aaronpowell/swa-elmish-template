# Azure Static Web Apps Fable Template

This repository contains a template for creating an [Azure Static Web App](https://docs.microsoft.com/azure/static-web-apps/?WT.mc_id=dotnet-0000-aapowell) projects using Fable, Paket and F# Azure Functions.

To get started, click the **Use this template** button to create a repository from this template, and check out the [GitHub docs on using templates](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-from-a-template).

## Running The Application

From within VS Code run the **Launch it all 🚀** Debug configuration to start the Fable app, Azure Functions, Static Web Apps CLI and debuggers.

It's recommended that you use a [VS Code Remote Container](https://code.visualstudio.com/docs/remote/containers?WT.mc_id=dotnet-00000-aapowell) for development, as it will setup all the required dependencies and VS Code extensions.

### Manual Environment Setup

If you don't wish to use a VS Code Remote Container you will need the following dependencies installed:

* .NET SDK 3.1
* Node.js 14
* [Azure Static Web Apps CLI](https://github.com/azure/static-web-apps-cli)
* [Azure Function Core Tools](https://github.com/Azure/azure-functions-core-tools)

Once the repo is created from the terminal run:

```bash
$> dotnet tool restore
$> dotnet paket install
$> npm install
$> npm install -g @azure/static-web-apps-cli azure-functions-core-tools@3
```

With all dependencies installed, you can launch the apps, which will require three terminals:

1. Termainl 1: `npm start`
1. Terminal 2: `cd api && func start`
1. Terminal 3: `swa start http://localhost:3000 --api http://localhost:7071`

Then you can navigate to `http://localhost:4280` to access the emulator.
