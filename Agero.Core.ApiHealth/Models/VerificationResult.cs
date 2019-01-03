using System.Runtime.Serialization;

namespace Agero.Core.ApiHealth.Models
{
    /// <summary>Health verification result</summary>
    [DataContract]
    public class VerificationResult
    {
        /// <summary>Verification type</summary>
        [DataMember(Name = "verificationType")]
        public string Type { get; set; }

        /// <summary>Verification success</summary>
        [DataMember(Name = "isSuccessful")]
        public bool Success { get; set; }

        /// <summary>Elapsed time in milliseconds</summary>
        [DataMember(Name = "elapsedTimeInMilliSeconds")]
        public long ElapsedTimeInMilliSeconds { get; set; }

        /// <summary>Verification description</summary>
        [DataMember(Name = "verificationDescription")]
        public string Description { get; set; }

        /// <summary>Verification error details</summary>
        [DataMember(Name = "errorDetails")]
        public string ErrorDetails { get; set; }
    }
}
