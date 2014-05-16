using ComboRox.Core.Pagination;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ComboRox.Core.UnitTests.Pagination
{
    [TestClass]
    public class PaginationModule_AppluExpression_Should
    {
        [TestMethod]
        public void ReturnCorrectCollection_WhenPassingPaginationObject()
        {
            // Arrange
            IModule paginationModule = new PaginationModule();
            var settings = new ModulesSettings
                {
                    Pagination = new PaginationObject
                    {
                        Page = 2,
                        PageSize = 2
                    }
                };
            var collection = new List<Filter> { new Filter(), new Filter(), new Filter(), new Filter(), new Filter() };

            // Act
            var result = paginationModule.ApplyExpression(collection, settings);
            
            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}