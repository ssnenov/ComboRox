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
            Assert.AreEqual(55, sortedCollection.Last().Age);
        }

        [TestMethod]
        public void ReturnSortedCollection_WhenThereAreTwoCriterias()
        {
            // Arrange
            var listOfItems = new List<UserTestingClass>
                {
                    new UserTestingClass { Age = 10, FirstName = "Eadgar" },
                    new UserTestingClass { Age = 33, FirstName = "Barny" },
                    new UserTestingClass { Age = 5, FirstName = "Danny" },
                    new UserTestingClass { Age = 5, FirstName = "Connie" },
                    new UserTestingClass { Age = 10, FirstName = "Angel" }
                };

            var modulesSettings = new ModulesSettings
            {
                Sorting =
                    new List<SortingObject>
                        {
                            new SortingObject { Prop = "Age", Direction = SortingDirection.Asc },
                            new SortingObject { Prop = "FirstName", Direction = SortingDirection.Desc }
                        }
            };

            var sortingModule = new SortingModule();

            // Act
            var sortedCollection = sortingModule.ApplyExpression(listOfItems, modulesSettings).ToList();

            // Assert
            Assert.IsTrue(sortedCollection.Any());
            Assert.AreEqual(5, sortedCollection.First().Age);
            Assert.AreEqual("Danny", sortedCollection.First().FirstName);

            Assert.AreEqual(10, sortedCollection[3].Age);
            Assert.AreEqual("Angel", sortedCollection[3].FirstName);

            Assert.AreEqual(33, sortedCollection.Last().Age);
            Assert.AreEqual("Barny", sortedCollection.Last().FirstName);
        }
    }
}