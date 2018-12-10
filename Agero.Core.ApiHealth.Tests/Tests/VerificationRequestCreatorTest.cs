using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Agero.Core.ApiHealth.Helpers;
using Agero.Core.ApiHealth.Models;
using Agero.Core.ApiHealth.Tests.Helpers;
using Agero.Core.ApiHealth.Tests.Models;
using Newtonsoft.Json;

namespace Agero.Core.ApiHealth.Tests
{
    [TestClass]
    public class VerificationRequestCreatorTest
    {
        private static ApiHealthTestsSetup _apiHealthTestsSetupInfo;

        [ClassInitialize]
        public static void LoggerTestsInitialize(TestContext context)
        {
            Assert.IsTrue(File.Exists(@"test-settings.json"), "The configuration file test-settings.json needs to be setup. Please see https://github.com/agero-core/api-health to set it up.");

            _apiHealthTestsSetupInfo = JsonConvert.DeserializeObject<ApiHealthTestsSetup>(File.ReadAllText(@"test-settings.json"));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task CreateAsyncHealthRequest_When_Request_Is_Health_Request()
        {
            // Arrange
            var applicationUri = new Uri(_apiHealthTestsSetupInfo.ApplicationUri);
            var headers = _apiHealthTestsSetupInfo.Headers;
            
            // Act
            var request = VerificationRequestCreator.CreateAsyncHealthRequest(ConstantHelper.VerificationType, applicationUri, HealthCheckMode.Full, ConstantHelper.VerificationDescription, headers, 5000);
            
            // Assert
            Assert.AreEqual(ConstantHelper.VerificationType, request.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, request.Description);

            var actionResult = await request.Action();
            Assert.IsNull(actionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void CreateSyncHealthRequest_When_Request_Is_Health_Request()
        {
            // Arrange
            var applicationUri = new Uri(_apiHealthTestsSetupInfo.ApplicationUri);
            var headers = _apiHealthTestsSetupInfo.Headers;

            // Act
            var request = VerificationRequestCreator.CreateSyncHealthRequest(ConstantHelper.VerificationType, applicationUri, HealthCheckMode.Full, ConstantHelper.VerificationDescription, headers, 5000);

            // Assert
            Assert.AreEqual(ConstantHelper.VerificationType, request.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, request.Description);

            var actionResult = request.Action();
            Assert.IsNull(actionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task CreateAsyncHttpRequest_When_Request_Is_Http_Request()
        {
            // Arrange
            var uri = new Uri("https://www.example.com");

            // Act
            var request = VerificationRequestCreator.CreateAsyncHttpRequest(ConstantHelper.VerificationType, uri, HttpStatusCode.OK, ConstantHelper.VerificationDescription, timeout: 5000);

            // Assert
            Assert.AreEqual(ConstantHelper.VerificationType, request.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, request.Description);

            var actionResult = await request.Action();
            Assert.IsNull(actionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void CreateSyncHttpRequest_When_Request_Is_Http_Request()
        {
            // Arrange
            var uri = new Uri("https://www.example.com");

            // Act
            var request = VerificationRequestCreator.CreateSyncHttpRequest(ConstantHelper.VerificationType, uri, HttpStatusCode.OK, ConstantHelper.VerificationDescription, timeout: 5000);

            // Assert
            Assert.AreEqual(ConstantHelper.VerificationType, request.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, request.Description);

            var actionResult = request.Action();
            Assert.IsNull(actionResult);
        }
    }
}
