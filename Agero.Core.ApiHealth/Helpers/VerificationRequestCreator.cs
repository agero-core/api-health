using Agero.Core.ApiHealth.Models;
using Agero.Core.Checker;
using Agero.Core.RestCaller;
using Agero.Core.RestCaller.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Agero.Core.ApiHealth.Helpers
{
    /// <summary>Helper for creating verification requests</summary>
    public static class VerificationRequestCreator
    {
        private static readonly IRESTCaller _restCaller = new RESTCaller();

        /// <summary>Creates async verification request to /health API which checks 'overallStatus' field</summary>
        /// <param name="type">Verification type</param>
        /// <param name="applicationUri">URL to application. /health resource will added automatically.</param>
        /// <param name="mode">HealthCheckMode Quick/Full</param>
        /// <param name="description">Verification description</param>
        /// <param name="headers">HTTP request headers</param>
        /// <param name="timeout">HTTP request timeout</param>
        /// <returns>Verification request</returns>
        public static AsyncVerificationRequest CreateAsyncHealthRequest(
            string type, Uri applicationUri, HealthCheckMode mode,
            string description = null, IReadOnlyDictionary<string, string> headers = null, int timeout = 60000)
        {
            Check.ArgumentIsNullOrWhiteSpace(type, "type");
            Check.ArgumentIsNull(applicationUri, "applicationUri");
            Check.Argument(timeout > 0, "timeout > 0");

            var uri = mode == HealthCheckMode.Quick ? applicationUri.Add("health?mode=quick") : applicationUri.Add("health");

            return
                new AsyncVerificationRequest
                (
                    type: type,
                    description: description,
                    action: async () =>
                    {
                        var response = await _restCaller.GetAsync(uri, headers: headers, timeout: timeout);

                        return ProcessResponseForHealthRequest(uri, response);
                    }
                );
        }

        /// <summary>Creates sync verification request to /health API which checks 'overallStatus' field</summary>
        /// <param name="type">Verification type</param>
        /// <param name="applicationUri">URL to application. /health resource will added automatically.</param>
        /// <param name="mode">HealthCheckMode Quick/Full</param>
        /// <param name="description">Verification description</param>
        /// <param name="headers">HTTP request headers</param>
        /// <param name="timeout">HTTP request timeout</param>
        /// <returns>Verification request</returns>
        public static SyncVerificationRequest CreateSyncHealthRequest(
            string type, Uri applicationUri, HealthCheckMode mode,
            string description = null, IReadOnlyDictionary<string, string> headers = null, int timeout = 60000)
        {
            Check.ArgumentIsNullOrWhiteSpace(type, "type");
            Check.ArgumentIsNull(applicationUri, "applicationUri");
            Check.Argument(timeout > 0, "timeout > 0");

            var uri = mode == HealthCheckMode.Quick ? applicationUri.Add("health?mode=quick") : applicationUri.Add("health");

            return
                new SyncVerificationRequest
                (
                    type: type,
                    description: description,
                    action: () =>
                    {
                        var response = _restCaller.Get(uri, headers: headers, timeout: timeout);

                        return ProcessResponseForHealthRequest(uri, response);
                    }
                );
        }

        private static string ProcessResponseForHealthRequest(Uri uri, RestCallerResponse response)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNull(response, "response");

            if (response.HttpStatusCode != HttpStatusCode.OK)
                return GetErrorText(uri, response.HttpStatusCode, response.Text);

            var health = JsonConvert.DeserializeObject<Health>(response.Text);
            if (!health.Success)
                return GetErrorText(uri, response.HttpStatusCode, response.Text);

            return null;
        }

        /// <summary>Creates async verification request for HTTP GET request</summary>
        /// <param name="type">Verification type</param>
        /// <param name="uri">URL to call.</param>
        /// <param name="expectedHttpStatusCode">Expected (good) HTTP status code</param>
        /// <param name="description">Verification description</param>
        /// <param name="headers">HTTP request headers</param>
        /// <param name="timeout">HTTP request timeout</param>
        /// <returns>Verification request</returns>
        public static AsyncVerificationRequest CreateAsyncHttpRequest(
            string type, Uri uri, HttpStatusCode expectedHttpStatusCode,
            string description = null, IReadOnlyDictionary<string, string> headers = null, int timeout = 60000)
        {
            Check.ArgumentIsNullOrWhiteSpace(type, "type");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout > 0, "timeout > 0");

            return
                new AsyncVerificationRequest
                (
                    type: type,
                    description: description,
                    action: async () =>
                    {
                        var response = await _restCaller.GetAsync(uri, headers: headers, timeout: timeout);

                        return ProcessResponseForHttpRequest(uri, response, expectedHttpStatusCode);
                    }
                );
        }

        /// <summary>Creates sync verification request for HTTP GET request</summary>
        /// <param name="type">Verification type</param>
        /// <param name="uri">URL to call.</param>
        /// <param name="expectedHttpStatusCode">Expected (good) HTTP status code</param>
        /// <param name="description">Verification description</param>
        /// <param name="headers">HTTP request headers</param>
        /// <param name="timeout">HTTP request timeout</param>
        /// <returns>Verification request</returns>
        public static SyncVerificationRequest CreateSyncHttpRequest(
            string type, Uri uri, HttpStatusCode expectedHttpStatusCode,
            string description = null, IReadOnlyDictionary<string, string> headers = null, int timeout = 60000)
        {
            Check.ArgumentIsNullOrWhiteSpace(type, "type");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout > 0, "timeout > 0");

            return
                new SyncVerificationRequest
                (
                    type: type,
                    description: description,
                    action: () =>
                    {
                        var response = _restCaller.Get(uri, headers: headers, timeout: timeout);

                        return ProcessResponseForHttpRequest(uri, response, expectedHttpStatusCode);
                    }
                );
        }

        private static string ProcessResponseForHttpRequest(Uri uri, RestCallerResponse response, HttpStatusCode expectedHttpStatusCode)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNull(response, "response");

            if (response.HttpStatusCode != expectedHttpStatusCode)
                return GetErrorText(uri, response.HttpStatusCode, response.Text);

            return null;
        }

        private static string GetErrorText(Uri uri, HttpStatusCode httpStatusCode, string body)
        {
            Check.ArgumentIsNull(uri, "uri");

            return $"GET HTTP request to '{uri}' returned '{httpStatusCode}' HTTP status and the following body: {body}.";
        }
    }
}
