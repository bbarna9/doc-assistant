using DocAssistant_Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Models
{
    [TestClass]
    public class AssistantTests
    {
        [TestMethod]
        public void CompareObjects_ShouldEqual()
        {
            // Arrange
            var assistant = new Assistant();
            assistant.Id = 1;

            var assistant2 = new Assistant();
            assistant2.Id = 1;
            // Act

            var result = assistant.Equals(assistant2);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CompareObjects_ShouldDiffer()
        {
            // Arrange
            var assistant = new Assistant();
            assistant.Id = 1;

            var assistant2 = new Assistant();
            assistant2.Id = 2;
            // Act

            var result = assistant.Equals(assistant2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}