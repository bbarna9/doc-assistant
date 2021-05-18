using System;
using DocAssistant_Common.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Attributes.Validation
{
    [TestClass]
    public class SSNValidationAttributeTest
    {
        [DataRow("123-456-789")]
        [DataRow("095-654-321")]
        [DataRow("651-841-111")]
        [TestMethod]
        public void ValidateObject_ShouldPassValidation(string ssn)
        {
            // Arrange
            var attribute = new SSNValidationAttribute();
            // Act
            var result = attribute.IsValid(ssn);
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("123-456-789-")]
        [DataRow("095_654_321")]
        [DataRow("AAA-841-111")]
        [TestMethod]
        public void ValidateObject_ShouldFailValidation(string ssn)
        {
            // Arrange
            var attribute = new SSNValidationAttribute();
            // Act
            var result = attribute.IsValid(ssn);
            // Assert
            Assert.IsFalse(result);
        }
    }
}