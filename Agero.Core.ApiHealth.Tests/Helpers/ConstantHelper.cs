using System;

namespace Agero.Core.ApiHealth.Tests.Helpers
{
    public static class ConstantHelper
    {
        public const string VerificationType = "TestType";
        public const string VerificationDescription = "TestDescription";

        public const string ApplicationName = "TestApplicationName";
        public const string ApplicationVersion = "TestApplicationVersion";

        public static readonly Uri RunbookUrl = new Uri("http://runbook/");

        public const string ErrorText = "TestErrorText";
    }
}
