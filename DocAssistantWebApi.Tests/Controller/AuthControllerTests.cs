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

        [TestMethod]
        public async Task AuthDoctor_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "doctor";
            var credentials = new Credentials();

            var mockDoctorAuthService = new Mock<IAuthService<Doctor>>();
            mockDoctorAuthService.Setup(mock => mock.Auth(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Doctor());
            
            var mockAssitantAuthService = new Mock<IAuthService<Assistant>>();
            
            var authController = new AuthController(mockDoctorAuthService.Object,mockAssitantAuthService.Object);

            // Act

            var result = await authController.Auth(type,credentials);

            // Assert

            var okResponse = result as OkObjectResult;
            
            Assert.IsNotNull(okResponse);

            var token = okResponse.Value;
            
            Assert.IsNotNull(token);
        }
        [TestMethod]
        public async Task AuthDoctor_ShouldFailAuth()
        {
            // Arrange

            var type = "doctor";
            var credentials = new Credentials();

            var mockDoctorAuthService = new Mock<IAuthService<Doctor>>();
            mockDoctorAuthService.Setup(mock => mock.Auth(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(null as Doctor);
            
            var mockAssitantAuthService = new Mock<IAuthService<Assistant>>();
            
            var authController = new AuthController(mockDoctorAuthService.Object,mockAssitantAuthService.Object);

            // Act

            var result = await authController.Auth(type,credentials);

            // Assert

            var okResponse = result as UnauthorizedResult;
            
            Assert.IsNotNull(okResponse);
        }
        [TestMethod]
        public async Task AuthAssistant_ShouldReturnOkResponse()
        {
            // Arrange

            var type = "assistant";
            var credentials = new Credentials();

            var mockDoctorAuthService = new Mock<IAuthService<Doctor>>();

            var mockAssitantAuthService = new Mock<IAuthService<Assistant>>();
            mockAssitantAuthService.Setup(mock => mock.Auth(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Assistant());
            
            var authController = new AuthController(mockDoctorAuthService.Object,mockAssitantAuthService.Object);

            // Act

            var result = await authController.Auth(type,credentials);

            // Assert

            var okResponse = result as OkObjectResult;
            
            Assert.IsNotNull(okResponse);

            var token = okResponse.Value;
            
            Assert.IsNotNull(token);
        }
        [TestMethod]
        public async Task AuthAssistant_ShouldFailAuth()
        {
            // Arrange

            var type = "doctor";
            var credentials = new Credentials();

            var mockDoctorAuthService = new Mock<IAuthService<Doctor>>();

            var mockAssitantAuthService = new Mock<IAuthService<Assistant>>();
            mockAssitantAuthService.Setup(mock => mock.Auth(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(null as Assistant);
            
            var authController = new AuthController(mockDoctorAuthService.Object,mockAssitantAuthService.Object);

            // Act

            var result = await authController.Auth(type,credentials);

            // Assert

            var okResponse = result as UnauthorizedResult;
            
            Assert.IsNotNull(okResponse);
        }
    }
}