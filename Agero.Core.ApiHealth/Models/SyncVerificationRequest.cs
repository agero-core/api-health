using System;

namespace Agero.Core.ApiHealth.Models
{
    /// <summary>Sync verification request</summary>
    public class SyncVerificationRequest : VerificationRequest<string>
    {
        /// <summary>Constructor</summary>
        /// <param name="type">Verification type</param>
        /// <param name="action">Verification action</param>
        /// <param name="description">Verification description</param>
        public SyncVerificationRequest(string type, Func<string> action, string description = null) 
            : base(type, action, description)
        {
        }
    }
}
