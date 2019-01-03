using Agero.Core.ApiHealth.Models;
using Agero.Core.Checker;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Agero.Core.ApiHealth.Helpers
{
    internal static class VerificationRequestHelper
    {
        public static async Task<VerificationResult> ExecuteAsync(VerificationRequest<Task<string>> request, bool includeErrorDetails)
        {
            Check.ArgumentIsNull(request, "request");
            Check.ArgumentIsNullOrWhiteSpace(request.Type, "request.Type");

            var watch = Stopwatch.StartNew();
            try
            {
                var errorDetails = await request.Action();

                watch.Stop();
                return CreateResult(request.Type, request.Description, watch.ElapsedMilliseconds, errorDetails, includeErrorDetails);
            }
            catch (Exception ex)
            {
                watch.Stop();
                return CreateErrorResult(request.Type, request.Description, watch.ElapsedMilliseconds, ex.ToString(), includeErrorDetails);
            }
        }

        public static VerificationResult Execute(VerificationRequest<string> request, bool includeErrorDetails)
        {
            Check.ArgumentIsNull(request, "request");
            Check.ArgumentIsNullOrWhiteSpace(request.Type, "request.Type");

            var watch = Stopwatch.StartNew();
            try
            {
                var errorDetails = request.Action();

                watch.Stop();

                return CreateResult(request.Type, request.Description, watch.ElapsedMilliseconds, errorDetails, includeErrorDetails);
            }
            catch (Exception ex)
            {
                watch.Stop();
                return CreateErrorResult(request.Type, request.Description, watch.ElapsedMilliseconds, ex.ToString(), includeErrorDetails);
            }
        }

        private static VerificationResult CreateResult(string type, string description, long elapsedMilliseconds, string errorDetails, bool includeErrorDetails)
        {
            return
                errorDetails == null
                    ? CreateSuccessResult(type, description, elapsedMilliseconds)
                    : CreateErrorResult(type, description, elapsedMilliseconds, errorDetails, includeErrorDetails);
        }

        private static VerificationResult CreateSuccessResult(string type, string description, long elapsedMilliseconds)
        {
            return
                new VerificationResult
                {
                    Type= type,
                    Success= true,
                    ElapsedTimeInMilliSeconds= elapsedMilliseconds,
                    Description= description,
                    ErrorDetails= null
                };
               
        }

        private static VerificationResult CreateErrorResult(string type, string description, long elapsedMilliseconds, string errorDetails, bool includeErrorDetails)
        {
            return
                new VerificationResult
                { 
                    Type= type,
                    Success= false,
                    ElapsedTimeInMilliSeconds = elapsedMilliseconds,
                    Description= description,
                    ErrorDetails= includeErrorDetails ? errorDetails : null
                };
        }
    }
}
