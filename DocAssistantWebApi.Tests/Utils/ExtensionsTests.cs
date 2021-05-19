using System.Collections.Generic;
using System.Linq;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DocAssistantWebApi.Tests.Utils
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void GetFormattedRoles_ShouldReturnFormattedEnumerable()
        {
            // Arrange
            Roles [] roles = new [] {Roles.Assistant, Roles.Doctor};
            // Act
            var formatted = roles.GetFormattedRoles();
            // Assert
            CollectionAssert.AreEquivalent(new List<string>{"assistant","doctor"},formatted.ToList());
        }
    }
}