using ComboRox.Core.Sorting;
using ComboRox.Models;
using ComboRox.Models.Enums;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ComboRox.Core.UnitTests.Sorting.SortingExpressionFactoryTests
{
    [TestClass]
    public class Initialize_Should
    {
        [TestMethod]
        public void SetSortingObjectCorrectly()
        {
            // Arrange
            var sortingModule = new SortingModule();
            var requestJson = new ComboRequestJson();
            var modulesSettings = new ModulesSettings();

            requestJson.Sorting = new List<SortingObject>
                {
                    new SortingObject { Direction = SortingDirection.Asc, Prop = "Test" }
                };

            // Act
            IModulesSettings result = sortingModule.Initialize(requestJson, modulesSettings);

            // Assert
            Assert.IsNotNull(result.Sorting);
            Assert.IsNotNull(result.Sorting[0]);
            Assert.AreEqual(result.Sorting[0].Direction, result.Sorting[0].Direction);
            Assert.AreEqual(result.Sorting[0].Prop, result.Sorting[0].Prop);
        }

        [TestMethod]
        public void ReturnsNull_WhenPassingNullSortingObjec()
        {
            // Arrange
            var sortingModule = new SortingModule();
            var requestJson = new ComboRequestJson();
            var modulesSettings = new ModulesSettings();

            // Act
            IModulesSettings result = sortingModule.Initialize(requestJson, modulesSettings);

            // Assert
            Assert.IsNull(result.Sorting);
        }
    }
}