using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Controllers;
using DocAssistantWebApi.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocAssistantWebApi.Tests
{
    [TestClass]
    public class AssistantControllerUnitTests
    {
        private MockRepository<Assistant> _mockAssistantRepository;
        private AssistantController _mockAssistantController;

        public AssistantControllerUnitTests()
        {
            _mockAssistantController = this.SetupDefaultController();
        }
        
        private AssistantController SetupDefaultController()
        {
            _mockAssistantRepository = new Mock<IRepository<Assistant>>();

            var assistantController = new AssistantController(_mockAssistantRepository.Object);
            assistantController.ControllerContext = new Mock<ControllerContext>().Object;
            assistantController.ControllerContext.HttpContext = new DefaultHttpContext();
            assistantController.HttpContext.Items = new Dictionary<object, object?> {{"Id", 1L}};
            
            return assistantController;
        }
        
        [DataRow("TestAssistant","testlowercasepassword")]
        [DataRow("TestAssistant2","TESTUPPERCASEPASSWORD")]
        [DataRow("test","testCamelCasePassword")]
        [DataRow("assistant","test@special_Char$")]
        [TestMethod]
        public void ValidateCredentials_ShouldPassValidation(string username, string password)
        {
            // Arrange
            var assistantCredentials = new Credentials
            {
                Username = username,
                Password = password
            };
            var validationContext = new ValidationContext(assistantCredentials);
            var validationResult = new List<ValidationResult>();
            
            // Act
            var result = Validator.TryValidateObject(assistantCredentials, validationContext,
                validationResult, validateAllProperties: true);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("TestAssistant","short")]
        [DataRow("TestAssistant2","123")]
        [DataRow("test","_2!")]
        [TestMethod]
        public void ValidateCredentials_ShouldFailValidation(string username, string password)
        {
            // Arrange
            var assistantCredentials = new Credentials
            {
                Username = username,
                Password = password
            };
            var validationContext = new ValidationContext(assistantCredentials);
            var validationResult = new List<ValidationResult>();
            
            // Act
            var result = Validator.TryValidateObject(assistantCredentials, validationContext,
                validationResult, validateAllProperties: true);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [DataRow("TestAssistant","testlowercasepassword")]
        [DataRow("TestAssistant2","TESTUPPERCASEPASSWORD")]
        [DataRow("test","testCamelCasePassword")]
        [DataRow("assistant","test@special_Char$")]
        [TestMethod]
        public async Task RegisterAssistant_ShouldPassRegistration(string username, string password)
        {
            // Arrange
            _mockAssistantRepository.Reset();
            
            var assistantCredentials = new Credentials
            {
                Username = username,
                Password = password
            };

            // Act
            var result = await _mockAssistantController.RegisterAssistant(assistantCredentials);
            
            // Assert
            Assert.AreEqual(typeof(OkResult),result.GetType());
        }
        [TestMethod]
        public async Task UpdateAssistantData_ShouldPassUpdate()
        {
            // Arrange
            var assistant = new Assistant();
            
            // Act
            var result = await _mockAssistantController.UpdateAssistantData(assistant);
            
            // Assert
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}