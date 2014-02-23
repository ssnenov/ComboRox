using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.UnitTests.Filters.FiltersBuilder
{
    [TestClass]
    public class FiltersBuilder_Greate_Should
    {
        [TestMethod]
        public void ReturnCorrectCollection()
        {
            // Arrange
            IComponentBuilder<Filter, FilterObject> filtersBuilder = new Core.Filters.FiltersBuilder();

            var filterObjects = new List<FilterObject>
                {
                    new FilterObject { op = "eq", prop = "FirstName", value = "Jhon" , or = new List<FilterObject>
                        {
                            new FilterObject { op = "neq", prop = "FirstName", value = "Peter" }
                        }},
                    new FilterObject { op = "cnt", prop = "LastName", value = "Brown" }
                };

            var expectedResult = new List<Filter>
                {
                    new Filter { Operator = Operator.Equals, PropertyName = "FirstName", Value = "Jhon", OrFilters = new List<Filter>
                        {
                            new Filter { Operator = Operator.NotEquals, PropertyName = "FirstName", Value = "Peter" }
                        }},
                        new Filter { Operator = Operator.Contains, PropertyName = "LastName", Value = "Brown" }
                };

            // Act
            List<Filter> result = filtersBuilder.Create(filterObjects);

            // Assert
            var firstFilterFromResult = result.First();
            var firstFilterFromExprextedResult = expectedResult.First();

            Assert.AreEqual(firstFilterFromExprextedResult.Operator, firstFilterFromResult.Operator);
            Assert.AreEqual(firstFilterFromExprextedResult.PropertyName, firstFilterFromResult.PropertyName);
            Assert.AreEqual(firstFilterFromExprextedResult.Value, firstFilterFromResult.Value);

            Assert.AreEqual(firstFilterFromExprextedResult.OrFilters.First().Operator, firstFilterFromResult.OrFilters.First().Operator);
            Assert.AreEqual(firstFilterFromExprextedResult.OrFilters.First().PropertyName, firstFilterFromResult.OrFilters.First().PropertyName);
            Assert.AreEqual(firstFilterFromExprextedResult.OrFilters.First().Value, firstFilterFromResult.OrFilters.First().Value);
        }
    }
}