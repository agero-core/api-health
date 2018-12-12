# Api Health

API Health is a library that determines API Health by getting the health of all the dependent API Components.

## Usage:

Please see [GettingStartedTests.cs](./Agero.Core.ApiHealth.Tests/Tests/GettingStartedTests.cs) for usage details.

**Note:** Please use **VerificationRequestCreator.CreateSyncHealthRequest**, **VerificationRequestCreator.CreateAsyncHealthRequest** only if your API conforms to the [health api design](./HealthApiDesign.md).

## Running Tests:

If you have a Restful API that conforms to the [Health API design](./HealthApiDesign.md). Create the json file **test-settings.json** with the below configuration under the projects [Agero.Core.ApiHealth.Tests](./Agero.Core.ApiHealth.Tests).

```json
{
  "ApplicationUri": "<Your application Url>",
  // Headers is optional.
  "Headers": {
      // Add one or more header key:value pairs as required. 
      "Key_1" : "Value_1",
      "Key_2" : "Value_2"
  }
}
```