using System.Threading.Tasks;
using Agero.Core.ApiHealth.Models;

namespace Agero.Core.ApiHealth
{
    /// <summary>Health service</summary>
    public interface IHealthService
    {
        /// <summary>Executes verification requests and returns results</summary>
        /// <param name="requests">Verification requests</param>
        /// <returns>Verification results</returns>
        /// <remarks>
        /// If action returns null and does not throw exception then verification result is success.
        /// If action returns any string value then verification result is not success and value is added to verification details.
        /// If action throws exception than verification result is not success and exception data added to verification details.
        /// </remarks>
        Task<Health> GetHealthAsync(params AsyncVerificationRequest[] requests);

        /// <summary>Executes verification requests and returns results</summary>
        /// <param name="requests">Verification requests</param>
        /// <returns>Verification results</returns>
        /// <remarks>
        /// If action returns null and does not throw exception then verification result is success.
        /// If action returns any string value then verification result is not success and value is added to verification details.
        /// If action throws exception than verification result is not success and exception data added to verification details.
        /// </remarks>
        Health GetHealth(params SyncVerificationRequest[] requests);
    }
}
