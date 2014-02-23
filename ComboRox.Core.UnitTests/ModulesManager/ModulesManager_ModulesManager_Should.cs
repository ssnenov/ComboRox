using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComboRox.Core.UnitTests.ModulesManager
{
    [TestClass]
    public class ModulesManager_ModulesManager_Should
    {
        [TestMethod]
        public void AddAdditionalModule_WhenPassingAdditionalModules()
        {
            // Arrange
            var listOfAdditionalModules = new List<IModule>(1);
            var additionalModule = new CustomTestingModule();
            listOfAdditionalModules.Add(additionalModule);
            
            // Act
            var modulesManager = new Core.ModulesManager(listOfAdditionalModules);

            // Assert
            Assert.IsNotNull(modulesManager.Modules.Last.Value as CustomTestingModule);
        }
    }
}