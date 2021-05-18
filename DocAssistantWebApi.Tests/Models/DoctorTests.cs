using DocAssistant_Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Models
{
    [TestClass]
    public class DoctorTests
    {
        [TestMethod]
        public void CompareObjects_ShouldEqual()
        {
            // Arrange
            var doctor = new Doctor();
            doctor.Id = 1;

            var doctor2 = new Doctor();
            doctor2.Id = 1;
            // Act

            var result = doctor.Equals(doctor2);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CompareObjects_ShouldDiffer()
        {
            // Arrange
            var doctor = new Doctor();
            doctor.Id = 1;

            var doctor2 = new Doctor();
            doctor2.Id = 2;
            // Act

            var result = doctor.Equals(doctor2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}