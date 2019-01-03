using Agero.Core.ApiHealth.Helpers;
using Agero.Core.ApiHealth.Models;
using Agero.Core.Checker;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agero.Core.ApiHealth
{
    /// <summary>Health service</summary>
    public class HealthService : IHealthService
    {
        /// <summary>Constructor</summary>
        /// <param name="name">Application name</param>
        /// <param name="version">Application version</param>
        /// <param name="includeErrorDetails">Specifies whether error details are included to verification results</param>
        /// <param name="runBookUrl">Application runbook URL</param>
        public HealthService(string name, string version, bool includeErrorDetails = true, Uri runBookUrl = null)
        {
            Check.ArgumentIsNullOrWhiteSpace(name, "name");
            Check.ArgumentIsNullOrWhiteSpace(version, "version");

            Name = name;
            Version = version;
            IncludeErrorDetails = includeErrorDetails;
            RunBookUrl = runBookUrl;
        }

        /// <summary>Application name</summary>
        public string Name { get; }

        /// <summary>Application version</summary>
        public string Version { get; }

        /// <summary>Specifies whether error details are included to verification results</summary>
        public bool IncludeErrorDetails { get; }

        /// <summary>Application runbook URL</summary>
        public Uri RunBookUrl { get; }

        /// <summary>Executes verification requests and returns results</summary>
        /// <param name="requests">Verification requests</param>
        /// <returns>Verification results</returns>
        /// <remarks>
        /// If action returns null and does not throw exception then verification result is success.
        /// If action returns any string value then verification result is not success and value is added to verification details.
        /// If action throws exception than verification result is not success and exception data added to verification details.
        /// </remarks>
        public async Task<Health> GetHealthAsync(params AsyncVerificationRequest[] requests)
        {
            Check.ArgumentIsNull(requests, "requests");
            Check.Argument(requests.Length > 0, "requests.Length > 0");

            var tasks = 
                requests
                    .Select(async r => await VerificationRequestHelper.ExecuteAsync(r, IncludeErrorDetails))
                    .ToArray();

            var verifications = await Task.WhenAll(tasks);

            return CreateHealth(verifications, verifications.Sum(v => v.ElapsedTimeInMilliSeconds));
        }

        /// <summary>Executes verification requests and returns results</summary>
        /// <param name="requests">Verification requests</param>
        /// <returns>Verification results</returns>
        /// <remarks>
        /// If action returns null and does not throw exception then verification result is success.
        /// If action returns any string value then verification result is not success and value is added to verification details.
        /// If action throws exception than verification result is not success and exception data added to verification details.
        /// </remarks>
        public Health GetHealth(params SyncVerificationRequest[] requests)
        {
            Check.ArgumentIsNull(requests, "requests");
            Check.Argument(requests.Length > 0, "requests.Length > 0");

            var verifications =
                requests.AsParallel()
                    .Select(r => VerificationRequestHelper.Execute(r, IncludeErrorDetails))
                    .ToArray();

            return CreateHealth(verifications, verifications.Sum(v => v.ElapsedTimeInMilliSeconds));
        }

        private Health CreateHealth(VerificationResult[] verifications, long executionTime)
        {
            Check.ArgumentIsNull(verifications, "verifications");
            Check.Argument(executionTime >= 0, "executionTime >= 0");

            return new Health
            {
                RunBookUrl = RunBookUrl,
                Verifications = verifications,
                Version = Version,
                Name = Name,
                ExecutionTimeInMilliseconds = executionTime,
                Time = DateTimeOffset.UtcNow,
                Success = verifications.All(v => v.Success)
            };
        }
    }
}
