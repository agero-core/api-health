using Agero.Core.ApiHealth.Helpers;
using Agero.Core.ApiHealth.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Agero.Core.ApiHealth.Tests
{
    [TestClass]
    public class GettingStartedTests
    {
        [TestMethod]
        public async Task AsynchronousUsage()
        {
            // Create instance of health service
            IHealthService service =
                new HealthService
                (
                    name: "Test Application",
                    version: "1.0.0.0",
                    includeErrorDetails: true,
                    runBookUrl: new Uri("http://example.com/runbook")
                );

            // Create an array of verification requests
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
                        description: "Checks some API with health enpoint.",
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

            // Execute all verification requests and compose health object
            Health health = await service.GetHealthAsync(verificationRequests);

            // Health object can be serialized to JSON which complies with Health API spec 
            var json = JsonConvert.SerializeObject(health);
        }

        [TestMethod]
        public void SynchronousUsage()
        {
            IHealthService service =
                new HealthService(
                    name: "Test Application",
                    version: "1.0.0.0",
                    includeErrorDetails: true,
                    runBookUrl: new Uri("http://example.com/runbook")
                );

            var verificationRequests =
                new[]
                {
                    new SyncVerificationRequest(
                        type: "check_something",
                        description: "Checks something.",
                        action: () => null
                    ),
                    VerificationRequestCreator.CreateSyncHealthRequest(
                        type: "check_some_api_with_health_endpoint",
                        description: "Checks some API with health endpoint.",
                        applicationUri: new Uri("https://example.com/someapi"),
                        mode: HealthCheckMode.Full
                    ),
                    VerificationRequestCreator.CreateSyncHttpRequest(
                        type: "check_some_api_returns_200",
                        description: "Checks some API returns 200 OK HTTP status code.",
                        uri: new Uri("https://example.com/someapi/info"),
                        expectedHttpStatusCode: HttpStatusCode.OK
                    )
                };

            Health health = service.GetHealth(verificationRequests);

            var json = JsonConvert.SerializeObject(health);
        }
    }
}
