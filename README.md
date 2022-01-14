# ServiceNow .NET Client

[![GitLab Pipeline Status](https://gitlab.com/rabbids-incubator/servicenow-dotnet-client/badges/main/pipeline.svg)](https://gitlab.com/rabbids-incubator/servicenow-dotnet-client/-/pipelines)

This is the codebase of .NET components (API & libraries) to integrate with [ServiceNow](https://www.servicenow.com/) from any system (Linux, MacOS, Windows).

## How to have a ServiceNow instance

### ServiceNow Developer program

* Register to [ServiceNow Developer program](https://developer.servicenow.com/dev.do) (free)

* Create a new user to authenticat REST API calls to ServiceNow
  * Open the instance "Application Navigator" (URL like "https://devXXXXX.service-now.com/")
  * In "User Administration" > "Users", create a new User with "Web service access only" checked
  * Open the newly created user and assign CMDB Roles

## How to get knowledge about ServiceNow

* [Community](./docs/community.md)
* [ServiceNow CMDB](./docs/servicenow-cmdb.md)
* [ServiceNow Resources](./docs/servicenow-resources.md)

## How to build the solution

All commands are to be ran from the root folder of the repository (where the sln file is).

### Requirements

* [git CLI](https://git-scm.com/)
* [.NET 6.0 SDK](https://dotnet.microsoft.com/download) (or above)
* (Optional) [Docker Engine](https://docs.docker.com/engine/install/ubuntu/)

### Cloning

```bash
# clones with HTTPS URL
git clone https://github.com/rabbids-incubator/servicenow-dotnet-client.git
```

### Build

```bash
# restores .NET packages
dotnet restore

# builds the .NET solution
dotnet build
```

### Configuration

* Create the file `src/ConsoleApp/Properties/launchSettings.json` 
(can be done from Visual Studio by opening `Properties` window of ConsoleApp project then `Debug` > `General`)

```json
# update with the values of your environment
{
  "profiles": {
    "ConsoleApp": {
      "commandName": "Project",
      "commandLineArgs": "-v",
      "environmentVariables": {
        "ServiceNow__RestApi__Username": "admin",
        "ServiceNow__RestApi__Password": "*********",
        "ServiceNow__RestApi__BaseUrl": "https://devxxxxx.service-now.com/api/now"
      }
    }
  }
}
```

* Create the file `src/WebApi/appsettings.Development.json`

```json
# update with the values of your environment
{
  "ServiceNow": {
    "RestApi": {
      "BaseUrl": "https://devxxxxx.service-now.com/api/now",
      "Username": "admin",
      "Password": ""*********"
    }
  }
}
```

### Run

```bash
# runs the Console project
dotnet run --project src/ConsoleApp

# runs the Console dll with options
dotnet src/ConsoleApp/bin/Debug/net6.0/RabbidsIncubator.ServiceNowClient.ConsoleApp.dll -v

# runs the Web Api project (will be accessible at https://localhost:7079/swagger)
dotnet run --project src/WebApi

# checks api health (should returned "Healthy")
curl -k https://localhost:7079/health
```

### Debug in Visual Studio 2022

* Add breakpoint(s) in the files
* Select `WebApi` in the startup project list
* Click on `Debug` > `Start Debugging` (`F5`)

### Quality

```bash
# review and update source files from the rules defined in .editorconfig file
dotnet format
```

### Docker image

```bash
# creates a Docker image
docker build . -t rabbidsincubator/servicenowclientapi -f src/WebApi/Dockerfile --no-cache

# (once) logs in remote Docker repository (Artifactory)
docker login devpro.jfrog.io

# tags the image
docker tag <IMAGE_ID> devpro.jfrog.io/rabbidsincubator-docker-local/servicenowclientapi

# pushes the image to the repository (Artifactory)
docker push devpro.jfrog.io/rabbidsincubator-docker-local/servicenowclientapi

# pulls the image from the repository (Artifactory)
docker pull devpro.jfrog.io/rabbidsincubator-docker-local/servicenowclientapi
```
