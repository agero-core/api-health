using System;
using System.Threading.Tasks;

namespace Agero.Core.ApiHealth.Models
{
    /// <summary>Async verification request</summary>
    public class AsyncVerificationRequest : VerificationRequest<Task<string>>
    {
        /// <summary>Constructor</summary>
        /// <param name="type">Verification type</param>
        /// <param name="action">Verification action</param>
        /// <param name="description">Verification description</param>
        public AsyncVerificationRequest(string type, Func<Task<string>> action, string description = null) 
            : base(type, action, description)
        {
        }
    }
}
