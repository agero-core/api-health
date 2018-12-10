using System;
using System.Linq;
using System.Threading.Tasks;
using Agero.Core.ApiHealth.Models;
using Agero.Core.ApiHealth.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agero.Core.ApiHealth.Tests
{
    [TestClass]
    public class HealthServiceTests
    {
        [TestMethod]
        public async Task GetHealthAsync_When_Request_Action_Returns_Null()
        {
            // Arrange
            Func<Task<string>> action = async () => await Task.FromResult((string)null);

            var request = new AsyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = await healthService.GetHealthAsync(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsTrue(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);
            
            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsTrue(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsNull(verification.ErrorDetails);
        }

        [TestMethod]
        public void GetHealth_When_Request_Action_Returns_Null()
        {
            // Arrange
            Func<string> action = () => null;

            var request = new SyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = healthService.GetHealth(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsTrue(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsTrue(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsNull(verification.ErrorDetails);
        }

        [TestMethod]
        public async Task GetHealthAsync_When_Request_Action_Returns_Not_Null()
        {
            // Arrange
            Func<Task<string>> action = async () => await Task.FromResult(ConstantHelper.ErrorText);

            var request = new AsyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = await healthService.GetHealthAsync(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.AreEqual(ConstantHelper.ErrorText, verification.ErrorDetails);
        }

        [TestMethod]
        public void GetHealth_When_Request_Action_Returns_Not_Null()
        {
            // Arrange
            Func<string> action = () => ConstantHelper.ErrorText;

            var request = new SyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = healthService.GetHealth(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.AreEqual(ConstantHelper.ErrorText, verification.ErrorDetails);
        }

        [TestMethod]
        public async Task GetHealthAsync_When_Request_Action_Throws_Exception()
        {
            // Arrange
            Func<Task<string>> action = () => { throw new InvalidOperationException(ConstantHelper.ErrorText); };

            var request = new AsyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = await healthService.GetHealthAsync(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsTrue(verification.ErrorDetails.Contains(ConstantHelper.ErrorText));
        }

        [TestMethod]
        public void GetHealth_When_Request_Action_Throws_Exception()
        {
            // Arrange
            Func<string> action = () => { throw new InvalidOperationException(ConstantHelper.ErrorText); };

            var request = new SyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, true, ConstantHelper.RunbookUrl);

            // Act
            var health = healthService.GetHealth(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsTrue(verification.ErrorDetails.Contains(ConstantHelper.ErrorText));
        }

        [TestMethod]
        public async Task GetHealthAsync_When_Request_Action_Returns_Not_Null_And_Include_Erro_Details_Is_False()
        {
            // Arrange
            Func<Task<string>> action = async () => await Task.FromResult(ConstantHelper.ErrorText);

            var request = new AsyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, false, ConstantHelper.RunbookUrl);

            // Act
            var health = await healthService.GetHealthAsync(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsNull(verification.ErrorDetails);
        }

        [TestMethod]
        public async Task GetHealthAsync_When_Request_Action_Throws_Exception_And_Include_Erro_Details_Is_False()
        {
            // Arrange
            Func<Task<string>> action = () => { throw new InvalidOperationException(ConstantHelper.ErrorText); };

            var request = new AsyncVerificationRequest(ConstantHelper.VerificationType, action, ConstantHelper.VerificationDescription);

            var healthService = new HealthService(ConstantHelper.ApplicationName, ConstantHelper.ApplicationVersion, false, ConstantHelper.RunbookUrl);

            // Act
            var health = await healthService.GetHealthAsync(request);

            // Assert
            Assert.IsNotNull(health);
            Assert.IsFalse(health.Success);
            Assert.AreEqual(ConstantHelper.ApplicationVersion, health.Version);
            Assert.AreEqual(ConstantHelper.ApplicationName, health.Name);
            Assert.IsTrue(health.ExecutionTimeInMilliseconds >= 0);
            Assert.IsTrue(DateTimeOffset.UtcNow.AddMinutes(-1) < health.Time && health.Time <= DateTimeOffset.UtcNow);
            Assert.AreEqual(ConstantHelper.RunbookUrl, health.RunBookUrl);

            Assert.IsNotNull(health.Verifications);
            Assert.AreEqual(1, health.Verifications.Length);

            var verification = health.Verifications.First();
            Assert.IsNotNull(verification);
            Assert.IsFalse(verification.Success);
            Assert.AreEqual(ConstantHelper.VerificationType, verification.Type);
            Assert.AreEqual(ConstantHelper.VerificationDescription, verification.Description);
            Assert.IsNull(verification.ErrorDetails);
        }
    }
}
