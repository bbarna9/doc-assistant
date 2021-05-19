using System;
using System.Text;
using DocAssistantWebApi.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Utils
{
    [TestClass]
    public class SecurityUtilsTests
    {
        [DataRow("test-password","01471167D9F30D8FFFB3","$10$01471167D9F30D8FFFB3$1890B3803C5680AB8ADCB421EF40D806B95121D99B92B50C366516026A777573")]
        [DataRow("R@ndomPasswordText$11!","7DF88D30584F3825956D","$10$7DF88D30584F3825956D$CF38F998148847ACF5EF2889A4F5E87484C357DF5F93D79F3A2BB8F2C75FBF55")]
        [DataRow("_password_text_!","9DF0CB851E37849FF1E0","$10$9DF0CB851E37849FF1E0$DEE9201ACCDD798A26CFF493D2C21E4310511010CD892D37CF62A73046573E75")]
        [TestMethod]
        public void CreatePasswordHash_ShouldReturnValidHash(string plain, string salt,string expected)
        {
            // Act
            var hash = SecurityUtils.CreatePasswordHash(plain, Convert.FromHexString(salt));
            // Assert
            Assert.AreEqual(expected,hash);
        }
        
        [DataRow("test-password","02471167D9F30D8FFFB3","$10$01471167D9F30D8FFFB3$1890B3803C5680AB8ADCB421EF40D806B95121D99B92B50C366516026A777573")]
        [DataRow("R@ndomPasswordText$11!","8DF88D30584F3825956D","$10$7DF88D30584F3825956D$CF38F998148847ACF5EF2889A4F5E87484C357DF5F93D79F3A2BB8F2C75FBF55")]
        [DataRow("_password_text_!","1DF0CB851E37849FF1E0","$10$9DF0CB851E37849FF1E0$DEE9201ACCDD798A26CFF493D2C21E4310511010CD892D37CF62A73046573E75")]
        [TestMethod]
        public void CreatePasswordHash_ShouldReturnInvalidHash(string plain, string salt,string expected)
        {
            // Act
            var hash = SecurityUtils.CreatePasswordHash(plain, Convert.FromHexString(salt));
            // Assert
            Assert.AreNotEqual(expected,hash);
        }

        [DataRow("test-password","$10$01471167D9F30D8FFFB3$1890B3803C5680AB8ADCB421EF40D806B95121D99B92B50C366516026A777573")]
        [DataRow("R@ndomPasswordText$11!","$10$7DF88D30584F3825956D$CF38F998148847ACF5EF2889A4F5E87484C357DF5F93D79F3A2BB8F2C75FBF55")]
        [DataRow("_password_text_!","$10$9DF0CB851E37849FF1E0$DEE9201ACCDD798A26CFF493D2C21E4310511010CD892D37CF62A73046573E75")]
        [TestMethod]
        public void VerifyPassword_ShouldBeValid(string plain, string hash)
        {
            // Act
            var result = SecurityUtils.VerifyPassword(plain, hash);
            // Assert
            Assert.IsTrue(result);
        }
        
        [DataRow("test-password","$10$01471167D9F30D8FFFB3$1890B3803C5680AB8ADCB421EF40D806B95121D99B92B50C366516026A777572")]
        [DataRow("R@ndomPasswordText$11!","$12$7DF88D30584F3825956D$CF38F998148847ACF5EF2889A4F5E87484C357DF5F93D79F3A2BB8F2C75FBF51")]
        [DataRow("_password_text_!","$10$9DF0CB851E37849FF1E0$DEE9201ACCDD798A26CFF493D2C21E4310511010CD892D37CF62A73046573E73")]
        [TestMethod]
        public void VerifyPassword_ShouldBeInvalid(string plain, string hash)
        {
            // Act
            var result = SecurityUtils.VerifyPassword(plain, hash);
            // Assert
            Assert.IsFalse(result);
        }
    }
}