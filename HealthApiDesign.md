# Health API (/health) design.

Health API design specification, specifies the standard to perform API health check through an API end point **(/health)** of a Restful Web Service.

To conform to the Health API design spec, the Restful API should support the below:

**Request:**

    HTTP Method: GET
    URL: /health
    Parameters : mode
    Parameter Values:  quick, full.

The Restful API should expose a HTTP GET, **/health** end point. 

The API can take a single query parameter, **mode**, which can take the below values - 

* **quick** : Used to check the health of critical dependent components that are must have.

* **full** : Used to check the health of all the components. Health check mode is full by default.

**Response:**

The API response body is a JSON object which conforms to [Health](./Agero.Core.ApiHealth/Models/Health.cs).

* **verifications** - Required array of the following JSON objects:
    * **verificationType** - Required string field which provides short description of verification.
    * **verificationDescription** - Optional string field which provides long description of verification.
    * **isSuccessful** - Required boolean field which indicates verification result.
    * **errorDetails** - Optional string field which provides error details in case of verification failure.
* **overallSuccess** - Required boolean field which indicates overall success of verifications.
* **name** - Optional string field which provides application name.
* **buildVersion** - Optional string field which provides application version.
* **time** - Optional string field which provides current time in ISO 8601 format.
* **executionTimeInMilliseconds** - Optional string field which provides total execution time of the health check in milliseconds.
* **runBookUrl** - Optional string field which provides URL to service runbook.

```json
{
  "verifications": [
    {
      "verificationType": "database_connection_check",
      "isSuccessful": true,
      "elapsedTimeInMilliSeconds": 209,
      "verificationDescription": "Database query succeeded.",
      "errorDetails": null
    },
    {
      "verificationType": "another_api_check",
      "isSuccessful": true,
      "elapsedTimeInMilliSeconds": 200,
      "verificationDescription": "Another API call returned 200.",
      "errorDetails": null
    }
  ],
  "overallSuccess": true,  
  "name": "Example API",  
  "buildVersion": "1.1.6.15",
  "time": "2016-12-05T16:46:05.5273163+00:00",
  "executionTimeInMilliseconds": 3618, 
  "runBookUrl": "http://example.com/runbook_url/"
}
```
