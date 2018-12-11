# Api Health

API Health is a **.NET Standard 2.0** compliant library that determines API Health by getting the health of all the dependent API Components.

# Usage:

Create instance:
```csharp
IHealthService service =
                new HealthService
                (
                    name: "Test Application",
                    version: "1.0.0.0",
                    includeErrorDetails: true,
                    runBookUrl: new Uri("http://example.com/runbook")
                );
```

Create an array of verification requests:
```csharp
var verificationRequests =
                new[]
                {
                    // Runs action and compose verification result based on response
                    new AsyncVerificationRequest
                    (
                        type: "check_something",
                        description: "Checks something.",
                        action:  () =>
                        {
                            // If any exception happens, than "isSuccessful" is false and exception details will be in "errorDetails".

                            // Return type is System.String
                            // If return value is null, than "isSuccessful" is true. 
                            // Otherwise "isSuccessful" is false and returned string will be in "errorDetails".  
                            return Task.FromResult<string>(null);
                        }
                    ),
                    // Calls health endpoint of provided API and composes verification result based on response
                    VerificationRequestCreator.CreateAsyncHealthRequest
                    (
                        type: "check_some_api_with_health_endpoint",
                        description: "Checks some API with health endpoint.",
                        applicationUri: new Uri("https://example.com/someapi"),
                        mode: HealthCheckMode.Full
                    ),
                    // Calls URL with GET method and composes verification result based on HTTP status code
                    VerificationRequestCreator.CreateAsyncHttpRequest
                    (
                        type: "check_some_api_returns_200",
                        description: "Checks some API returns 200 OK HTTP status code.",
                        uri: new Uri("https://example.com/someapi/info"),
                        expectedHttpStatusCode: HttpStatusCode.OK
                    )
                };
```

Execute all verification requests and compose health object:
```csharp
Health health = await service.GetHealthAsync(verificationRequests);
```

Health object can be serialized to JSON which complies with Health API spec: 
```csharp
var json = JsonConvert.SerializeObject(health);
```

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