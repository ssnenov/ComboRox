using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ComboRox.Core.UnitTests.ModulesManager
{
    [TestClass]
    public class ModulesManager_ApplyModulesExpressions_Should
    {
        [TestMethod]
        public void Should_ApplyExpressionSuccessfully()
        {
            // Arrange
            var modulesManager = new Core.ModulesManager();
            var comboRequestJson = new ComboRequestJson
            {
                Filters = new List<FilterObject>
                    {
                    new FilterObject { op = "eq", prop = "Page", value = 2}
                }
            };

            var collection = new List<PaginationObject>
                {
                    new PaginationObject { Page = 1, PageSize = 10},
                    new PaginationObject { Page = 2, PageSize = 10}
                };

            // Act
            IResultData resultData = modulesManager.ApplyModulesExpressions(collection, comboRequestJson);

            // Assert
            IResultData expected = new ResultData
                {
                    Data = new List<PaginationObject>
                    {
                        new PaginationObject { Page = 2, PageSize = 10}
                    }
                };
            int expectedCount = expected.Data.Cast<PaginationObject>().Count();
            int actualCount = resultData.Data.Cast<PaginationObject>().Count();

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}