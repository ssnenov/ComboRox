using ComboRox.Core.Sorting;
using ComboRox.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ComboRox.Core.UnitTests.Sorting.SortingModuleTests
{
    [TestClass]
    public class ConstructResult_Should
    {
        [TestMethod]
        public void SetCollection()
        {
            // Arrange
            var sortingModule = new SortingModule();
            var listOfItems = new List<UserTestingClass> 
            { 
                new UserTestingClass
                {
                    Id = Guid.NewGuid()
                }
            };

            var resultData = new ResultData();

            // Act
            IResultData result = sortingModule.ConstructResult(listOfItems, resultData);

            // Assert
            Assert.IsNotNull(result);

            var firstDataItem = result.Data as List<UserTestingClass>;

            Assert.AreEqual(listOfItems[0].Id, firstDataItem[0].Id);
        }
    }
}