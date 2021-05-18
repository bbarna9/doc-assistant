using DocAssistant_Common.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Attributes.Validation
{
    [TestClass]
    public class CommonValidationAttributeTest
    {

        [DataRow("^.{1,250}$","This sentence should be accepted.")]
        [DataRow("^[a-zA-Z0-9_\\- ]+$","123-AAA-BBB_120")]
        [TestMethod]
        public void ValidateObject_ShouldPassValidation(string pattern, string testCase)
        {
            // Arrange
            var attribute = new CommonValidationAttribute(pattern);
            // Act
            var result = attribute.IsValid(testCase);
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("^.{1,10}$","This sentence should not be accepted.")]
        [DataRow("^[a-zA-Z0-9]+$","123-AAA-BBB_120")]
        [TestMethod]
        public void ValidateObject_ShouldFailValidation(string pattern, string testCase)
        {
            // Arrange
            var attribute = new CommonValidationAttribute(pattern);
            // Act
            var result = attribute.IsValid(testCase);
            // Assert
            Assert.IsFalse(result);
        }
    }
}