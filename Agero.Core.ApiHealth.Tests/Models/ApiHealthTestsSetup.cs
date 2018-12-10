using System.Collections.Generic;

namespace Agero.Core.ApiHealth.Tests.Models
{
    internal class ApiHealthTestsSetup
    {
        public string ApplicationUri { get; set; }

        public Dictionary<string,string> Headers { get; set; }
    }
}