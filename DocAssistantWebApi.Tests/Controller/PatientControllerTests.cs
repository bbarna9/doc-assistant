using System;
using System.Collections.Generic;
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
    public class PatientControllerTests
    {
        private Mock<IRepository<Patient>> _mockPatientRepository;
        private Mock<IRepository<Assistant>> _mockAssistantRepository;
        private Mock<PatientController> _patientController;

        public PatientControllerTests()
        {
            _mockPatientRepository = new Mock<IRepository<Patient>>();
            _mockAssistantRepository = new Mock<IRepository<Assistant>>();

            _patientController = new Mock<PatientController>(_mockPatientRepository.Object,_mockAssistantRepository.Object);
        }

        [TestMethod]
        public async Task LoadPatient_ShouldLoadSingleEntity()
        {
            // Arrange
            var type = "single";
            var patientId = 1L;

            _patientController.Setup(mock => mock.LoadPatient(type, 1L))
               .ReturnsAsync(new OkResult());
            
            // Act
            var result = await _patientController.Object.LoadPatient(type, patientId);

            // Assert
            Assert.AreEqual(typeof(OkResult),result.GetType());
        }
        
        [TestMethod]
        public async Task LoadPatient_ShouldNotLoadSingleEntity()
        {
            // Arrange
            var type = "single";
            var patientId = 1L;

            _patientController.Setup(mock => mock.LoadPatient(type, 1L))
                .ReturnsAsync(new BadRequestResult());
            
            // Act
            var result = await _patientController.Object.LoadPatient(type, patientId);

            // Assert
            Assert.AreEqual(typeof(BadRequestResult),result.GetType());
        }
        
        [TestMethod]
        public async Task LoadPatient_ShouldLoadAllEntity()
        {
            // Arrange
            var type = "all";

            _patientController.Setup(mock => mock.LoadPatient(type, null))
                .ReturnsAsync(new OkResult());
            
            // Act
            var result = await _patientController.Object.LoadPatient(type,null);

            // Assert
            Assert.AreEqual(typeof(OkResult),result.GetType());
        }
        [TestMethod]
        public async Task LoadPatient_ShouldNotLoadAllEntity()
        {
            // Arrange
            var type = "all";

            _patientController.Setup(mock => mock.LoadPatient(type, null))
                .ReturnsAsync(new BadRequestResult());
            
            // Act
            var result = await _patientController.Object.LoadPatient(type,null);

            // Assert
            Assert.AreEqual(typeof(BadRequestResult),result.GetType());
        }
    }
}