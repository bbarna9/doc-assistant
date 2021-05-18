using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Controllers;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocAssistantWebApi.Tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<AuthController> _authController;

        public AuthControllerTests()
        {
            var mockDoctorAuthService = new Mock<IAuthService<Doctor>>();
            var mockAssistantAuthService = new Mock<IAuthService<Assistant>>();

            _authController = new Mock<AuthController>(mockDoctorAuthService.Object,mockAssistantAuthService.Object);
        }

        [TestMethod]
        public async Task AuthDoctor_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "doctor";
            var credentials = new Credentials();
            
            _authController.Setup(mock => mock.Auth(type, credentials))
               .ReturnsAsync(new OkResult());
            
            // Act

            var result = await _authController.Object.Auth(type,credentials);

            // Assert
            
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
        [TestMethod]
        public async Task AuthDoctor_ShouldReturnBadRequestResponse()
        {
            // Arrange

            var type = "doctor";
            var credentials = new Credentials();
            
            _authController.Setup(mock => mock.Auth(type, credentials))
                .ReturnsAsync(new BadRequestResult());
            
            // Act

            var result = await _authController.Object.Auth(type,credentials);

            // Assert
            
            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        [TestMethod]
        public async Task AuthAssistant_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "assistant";
            var credentials = new Credentials();
            
            _authController.Setup(mock => mock.Auth(type, credentials))
                .ReturnsAsync(new OkResult());
            
            // Act

            var result = await _authController.Object.Auth(type,credentials);

            // Assert
            
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
        [TestMethod]
        public async Task AuthAssistant_ShouldReturnBadRequestResponse()
        {
            // Arrange

            var type = "assistant";
            var credentials = new Credentials();
            
            _authController.Setup(mock => mock.Auth(type, credentials))
                .ReturnsAsync(new BadRequestResult());
            
            // Act

            var result = await _authController.Object.Auth(type,credentials);

            // Assert
            
            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        
        [TestMethod]
        public async Task LogOutDoctor_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "doctor";

            _authController.Setup(mock => mock.Logout(type))
                .ReturnsAsync(new OkResult());
            
            // Act

            var result = await _authController.Object.Logout(type);

            // Assert
            
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
        
        [TestMethod]
        public async Task LogOutDoctor_ShouldReturnBadRequestResponse()
        {
            // Arrange

            var type = "doctor";

            _authController.Setup(mock => mock.Logout(type))
                .ReturnsAsync(new BadRequestResult());
            
            // Act

            var result = await _authController.Object.Logout(type);

            // Assert
            
            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        
        [TestMethod]
        public async Task LogOutAssistant_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "assistant";

            _authController.Setup(mock => mock.Logout(type))
                .ReturnsAsync(new OkResult());
            
            // Act

            var result = await _authController.Object.Logout(type);

            // Assert
            
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
        [TestMethod]
        public async Task LogOutAssistant_ShouldReturnBadRequestResponse()
        {
            // Arrange

            var type = "assistant";

            _authController.Setup(mock => mock.Logout(type))
                .ReturnsAsync(new BadRequestResult());
            
            // Act

            var result = await _authController.Object.Logout(type);

            // Assert
            
            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
    }
}