# Api Health

API Health is a library that determines API Health by getting the health of all the dependent API Components.

## Usage:

Please see below pages for examples of Asynchronous and Synchronous use cases of **Api Health**.

* [Asynchronous](./Docs/Asynchronous.md)
* [Synchronous](./Docs/Synchronous.md)

## Health API(/health) design:

Please use **VerificationRequestCreator.CreateSyncHealthRequest** or **VerificationRequestCreator.CreateAsyncHealthRequest** only if your API conforms to the [health api design](./HealthAPI/HealthApiDesign.md).

## Running Tests:

Create the json file **test-settings.json** with the below configuration under the projects **Agero.Core.ApiHealth.Tests**.

```json
{
  "ApplicationUri": "<Your application Url>",
  "Headers": {
      "Key_1" : "Value_1",
      "Key_2" : "Value_2",
      ...
  }
}
```

## Dependencies

* [Checker](https://github.com/agero-core/checker)
* [RestCaller](https://github.com/agero-core/rest-caller)