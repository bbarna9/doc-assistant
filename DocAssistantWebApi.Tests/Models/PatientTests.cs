using DocAssistant_Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Models
{
    [TestClass]
    public class PatientTests
    {
        [TestMethod]
        public void CompareObjects_ShouldEqual()
        {
            // Arrange
            var patient = new Patient();
            patient.Id = 1;

            var patient2 = new Patient();
            patient2.Id = 1;
            // Act

            var result = patient.Equals(patient2);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CompareObjects_ShouldDiffer()
        {
            // Arrange
            var patient = new Patient();
            patient.Id = 1;

            var patient2 = new Patient();
            patient2.Id = 2;
            // Act

            var result = patient.Equals(patient2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}