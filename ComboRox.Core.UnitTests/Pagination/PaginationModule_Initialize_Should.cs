using System.Collections.Generic;
using ComboRox.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComboRox.Core.Pagination;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.UnitTests.Pagination
{
    [TestClass]
    public class PaginationModule_Initialize_Should
    {
        [TestMethod]
        public void ReturnNotNull_WhenPassingCorrectParameters()
        {
            // Arrange
            IModule paginationModule = new PaginationModule();
            var requestJson = new ComboRequestJson()
            {
                Filters = new List<FilterObject>(),
                Pagination = new PaginationObject()
            };
            var settings = new ModulesSettings();

            // Act
            var result = paginationModule.Initialize(requestJson, settings);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DontSetPaginationObject_WhenPaginationObjectInRequestIsNull()
        {
            // Arrange
            IModule paginationModule = new PaginationModule();
            var requestJson = new ComboRequestJson()
                {
                    Filters = new List<FilterObject>(),
                    Pagination = null
                };
            var settings = new ModulesSettings();

            // Act
            var result = paginationModule.Initialize(requestJson, settings);

            // Assert
            Assert.IsNull(result.Pagination);
        }
    }
}