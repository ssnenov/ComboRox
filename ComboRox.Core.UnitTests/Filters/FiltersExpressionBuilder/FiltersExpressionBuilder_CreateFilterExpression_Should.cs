using System;
using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ComboRox.Core.UnitTests.Filters.FiltersExpressionBuilder
{
    [TestClass]
    public class FiltersExpressionBuilder_CreateFilterExpression_Should : FiltersExpressionBuilderTestsBase
    {
        [TestMethod]
        public void ReturnZeroItems_AfterFiltering()
        {
            // Arrange
            Expression<Func<FilterObject, bool>> filterExpression = Core.Filters.FiltersExpressionBuilder.Create<FilterObject>(new List<Filter>
                {
                    new Filter { Operator = Operator.Equals, PropertyName = "prop", Value = "FirstName" },
                    new Filter { Operator = Operator.Equals, PropertyName = "prop", Value = "LastName" }
                });

            // Act
            var result = collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ReturnCorrectExpression_WhenPassingOrExpresionFilters()
        {
            // Arrange
            Expression<Func<FilterObject, bool>> filterExpression = Core.Filters.FiltersExpressionBuilder.Create<FilterObject>(new List<Filter>
                {
                    new Filter
                        {
                        Operator = Operator.Equals, 
                        PropertyName = "prop",
                        Value = "FirstName",
                        OrFilters = new List<Filter> { new Filter { Operator = Operator.Equals, PropertyName = "prop", Value = "LastName" } }
                    }
                });

            // Act
            var result = collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ReturnCorrectExpression_WhenPassingEqualsNotEqualsExpresionFilters()
        {
            // Arrange
            Expression<Func<FilterObject, bool>> filterExpression = Core.Filters.FiltersExpressionBuilder.Create<FilterObject>(new List<Filter>
                {
                    new Filter
                        {
                        Operator = Operator.Equals, 
                        PropertyName = "prop",
                        Value = "FirstName"
                    },
                    new Filter { 
                        Operator = Operator.NotEquals,
                        PropertyName = "prop",
                        Value = "LastName" }
                });

            // Act
            var result = collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(1, result.Count());
        }
    }
}