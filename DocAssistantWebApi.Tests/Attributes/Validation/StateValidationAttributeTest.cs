using DocAssistant_Common.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Attributes.Validation
{
    [TestClass]
    public class StateValidationAttributeTest
    {
        private StateValidationAttribute _cityValidationAttribute;
        
        [TestInitialize]
        public void Setup()
        {
            _cityValidationAttribute = new StateValidationAttribute(minLength:1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'} );
        }
        
        [DataRow("California")]
        [DataRow("Hajdu-Bihar")]
        [TestMethod]
        public void ValidateObject_ShouldPassValidation(string state)
        {
            // Act
            var result = _cityValidationAttribute.IsValid(state);
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("C4liforni@")]
        [DataRow("Hajdu_Bihar")]
        [TestMethod]
        public void ValidateObject_ShouldFailValidation(string city)
        {
            // Act
            var result = _cityValidationAttribute.IsValid(city);
            // Assert
            Assert.IsFalse(result);
        }
    }
}