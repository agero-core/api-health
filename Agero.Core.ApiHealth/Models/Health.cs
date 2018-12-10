using System;
using System.Runtime.Serialization;

namespace Agero.Core.ApiHealth.Models
{
    /// <summary>Overall health</summary>
    [DataContract]
    public class Health
    {
        /// <summary>Health verification results</summary>
        [DataMember(Name = "verifications")]
        public VerificationResult[] Verifications { get; set; }

        /// <summary>Application version</summary>
        [DataMember(Name = "buildVersion")]
        public string Version { get; set; }

        /// <summary>Application name</summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>Current time</summary>
        [DataMember(Name = "time")]
        public DateTimeOffset Time { get; set; }

        /// <summary>Overall success</summary>
        [DataMember(Name = "overallSuccess")]
        public bool Success { get; set; }

        /// <summary>Overall executiontime in milliseconds</summary>
        [DataMember(Name = "executionTimeInMilliseconds")]
        public long ExecutionTimeInMilliseconds { get; set; }

        /// <summary>Runbook URL</summary>
        [DataMember(Name = "runBookUrl")]
        public Uri RunBookUrl { get; set; }
    }
}
