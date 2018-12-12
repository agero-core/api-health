# Api Health

[![NuGet Version](http://img.shields.io/nuget/v/Agero.Core.ApiHealth.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.ApiHealth/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.Core.ApiHealth.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.ApiHealth/)

API Health is a library that determines API Health by getting the health of all the dependent API Components.

## Usage:

Please see [GettingStartedTests.cs](./Agero.Core.ApiHealth.Tests/Tests/GettingStartedTests.cs) for usage details.

**Note:** Please use **VerificationRequestCreator.CreateSyncHealthRequest**, **VerificationRequestCreator.CreateAsyncHealthRequest** only if your API conforms to the [health api design](./HealthApiDesign.md).

## Running Tests:

If you have a Restful API that conforms to the [Health API design](./HealthApiDesign.md), create the json file **test-settings.json** with the below configuration under the projects [Agero.Core.ApiHealth.Tests](./Agero.Core.ApiHealth.Tests).

```json
{
  "ApplicationUri": "<Your application Url>",
  "Headers": {
      "Key_1" : "Value_1",
      "Key_2" : "Value_2"
  }
}
```