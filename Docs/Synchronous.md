# Synchronous Usage

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

Please use **VerificationRequestCreator.CreateSyncHealthRequest** or **VerificationRequestCreator.CreateAsyncHealthRequest** only if your API conforms to the [health api design](./HealthAPI/HealthApiDesign.md).


Create an array of verification requests:
```csharp
var verificationRequests =
                new[]
                {
                    // Runs action and compose verification result based on response
                    new SyncVerificationRequest
                    (
                        type: "check_something",
                        description: "Checks something.",
                        action:  () =>
                        {
                            // If any exception happens, than "isSuccessful" is false and exception details will be in "errorDetails".

                            // Return type is System.String
                            // If return value is null, than "isSuccessful" is true. 
                            // Otherwise "isSuccessful" is false and returned string will be in "errorDetails".  
                            return null;
                        }
                    ),
                    // Calls health endpoint of provided API and composes verification result based on response
                    VerificationRequestCreator.CreateSyncHealthRequest
                    (
                        type: "check_some_api_with_health_endpoint",
                        description: "Checks some API with health endpoint.",
                        applicationUri: new Uri("https://example.com/someapi"),
                        mode: HealthCheckMode.Full
                    ),
                    // Calls URL with GET method and composes verification result based on HTTP status code
                    VerificationRequestCreator.CreateSyncHttpRequest
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
Health health = await service.GetHealth(verificationRequests);
```

Health object can be serialized to JSON which complies with Health API spec: 
```csharp
var json = JsonConvert.SerializeObject(health);
```