using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ComboRox.Core.UnitTests.ModulesManager
{
    [TestClass]
    public class ModulesManager_GetModulesSettings_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowException_WhenPassingNullComboRequestJson()
        {
            // Arrange
            var modulesManager = new Core.ModulesManager();

            // Act
            modulesManager.GetModulesSettings(null, null);
        }

        [TestMethod]
        public void ReturnModulesSettingsObj_WhenPassingCorrectParameters()
        {
            // Arrange
            var modulesManager = new Core.ModulesManager();
            var comboRequestJson = new ComboRequestJson { Filters = new List<FilterObject>() };

            // Act
            IModulesSettings result = modulesManager.GetModulesSettings(comboRequestJson, null);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}