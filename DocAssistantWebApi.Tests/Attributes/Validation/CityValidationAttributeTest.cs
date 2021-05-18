using DocAssistant_Common.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Attributes.Validation
{
    [TestClass]
    public class CityValidationAttributeTest
    {

        private CityValidationAttribute _cityValidationAttribute;
        
        [TestInitialize]
        public void Setup()
        {
            _cityValidationAttribute = new CityValidationAttribute(minLength:1, maxLength: 255, invalidCharacters: new char[] {'~','!','@','#','$','%','^','&','*','(',')','_','=','+','{','}',';','\"','<','>','?','\\','/','.','|'} );
        }
        
        [DataRow("Los Angeles")]
        [DataRow("Debrecen")]
        [TestMethod]
        public void ValidateObject_ShouldPassValidation(string city)
        {
            // Act
            var result = _cityValidationAttribute.IsValid(city);
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("Los @ngeles")]
        [DataRow("Debrecen_")]
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