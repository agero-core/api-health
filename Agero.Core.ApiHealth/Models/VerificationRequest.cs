using Agero.Core.Checker;
using System;

namespace Agero.Core.ApiHealth.Models
{
    /// <summary>Verification request</summary>
    public class VerificationRequest<TActionResultType>
    {
        /// <summary>Constructor</summary>
        /// <param name="type">Verification type</param>
        /// <param name="action">Verification action</param>
        /// <param name="description">Verification description</param>
        public VerificationRequest(string type, Func<TActionResultType> action, string description = null)
        {
            Check.ArgumentIsNullOrWhiteSpace(type, "type");
            Check.ArgumentIsNull(action, "action");

            Type = type;
            Action = action;
            Description = description;
        }

        /// <summary>Verification type</summary>
        public string Type { get; }

        /// <summary>Verification action</summary>
        public Func<TActionResultType> Action { get; }

        /// <summary>Verification description</summary>
        public string Description { get; }
    }
}
