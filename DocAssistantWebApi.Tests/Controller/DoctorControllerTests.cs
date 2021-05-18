using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocAssistantWebApi.Tests
{
    [TestClass]
    public class DoctorControllerTests
    {
        private MockRepository<Doctor> _mockDoctorRepository;
        private MockRepository<Patient> _mockPatientRepository;
        private DoctorController _mockDoctorController;

        public DoctorControllerTests()
        {
            _mockDoctorRepository = new MockRepository<Doctor>();
            _mockPatientRepository = new MockRepository<Patient>();
            _mockDoctorController = SetupDefaultController();
        }
        
        private DoctorController SetupDefaultController()
        {
            var doctorController = new DoctorController(_mockDoctorRepository,_mockPatientRepository);
            doctorController.ControllerContext = new Mock<ControllerContext>().Object;
            doctorController.ControllerContext.HttpContext = new DefaultHttpContext();
            doctorController.HttpContext.Items = new Dictionary<object, object?>
            {
                {"Id", 1L},
                {"AccountType",Filters.Roles.Doctor}
            };
            
            return doctorController;
        }
        
        [DataRow("TestDoctor","testlowercasepassword")]
        [DataRow("TestDoctor2","TESTUPPERCASEPASSWORD")]
        [DataRow("test","testCamelCasePassword")]
        [DataRow("doctor","test@special_Char$")]
        [TestMethod]
        public void ValidateCredentials_ShouldPassValidation(string username, string password)
        {
            // Arrange
            var doctorCredentials = new Credentials
            {
                Username = username,
                Password = password
            };
            var validationContext = new ValidationContext(doctorCredentials);
            var validationResult = new List<ValidationResult>();
            
            // Act
            var result = Validator.TryValidateObject(doctorCredentials, validationContext,
                validationResult, validateAllProperties: true);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("TestDoctor","short")]
        [DataRow("TestDoctor2","123")]
        [DataRow("test","_2!")]
        [TestMethod]
        public void ValidateCredentials_ShouldFailValidation(string username, string password)
        {
            // Arrange
            var doctorCredentials = new Credentials
            {
                Username = username,
                Password = password
            };
            var validationContext = new ValidationContext(doctorCredentials);
            var validationResult = new List<ValidationResult>();
            
            // Act
            var result = Validator.TryValidateObject(doctorCredentials, validationContext,
                validationResult, validateAllProperties: true);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [DataRow("TestDoctor","testlowercasepassword")]
        [DataRow("TestDoctor2","TESTUPPERCASEPASSWORD")]
        [DataRow("test","testCamelCasePassword")]
        [DataRow("doctor","test@special_Char$")]
        [TestMethod]
        public async Task RegisterDoctor_ShouldPassRegistration(string username, string password)
        {
            // Arrange
            var doctorCredentials = new Credentials
            {
                Username = username,
                Password = password
            };

            // Act
            var result = await _mockDoctorController.Register(doctorCredentials);
            
            // Assert
            Assert.AreEqual(typeof(OkResult),result.GetType());
        }
        [TestMethod]
        public async Task UpdateDoctorData_ShouldPassUpdate()
        {
            // Arrange
            var doctor = new Doctor();
            
            // Act
            var result = await _mockDoctorController.UpdateDoctorData(doctor);
            
            // Assert
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}