using ComboRox.Core.Sorting;
using ComboRox.Models;
using ComboRox.Models.Enums;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ComboRox.Core.UnitTests.Sorting.SortingExpressionFactoryTests
{
    [TestClass]
    public class ApplyExpression_Should
    {
        [TestMethod]
        public void ReturnSortedCollection()
        {
            // Arrange
            var listOfItems = new List<UserTestingClass>
                {
                    new UserTestingClass { Age = 10 },
                    new UserTestingClass { Age = 33 },
                    new UserTestingClass { Age = 5 },
                    new UserTestingClass { Age = 55 },
                    new UserTestingClass { Age = 15 }
                };
            var modulesSettings = new ModulesSettings
                {
                    Sorting =
                        new List<SortingObject> { new SortingObject { Prop = "Age", Direction = SortingDirection.Asc } }
                };
            var sortingModule = new SortingModule();
            
            // Act
            var sortedCollection = (IOrderedEnumerable<UserTestingClass>)sortingModule.ApplyExpression(listOfItems, modulesSettings);

            // Assert
            Assert.IsTrue(sortedCollection.Any());
            Assert.AreEqual(5, sortedCollection.First().Age);
        }
    }
}