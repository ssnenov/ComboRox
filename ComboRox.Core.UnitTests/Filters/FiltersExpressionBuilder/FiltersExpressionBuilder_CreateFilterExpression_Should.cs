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
        public void ReturnCorrectExpression_WhenPassingOrExpresionFiltersAndPropertiesToParse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            DateTime dateTimeNow = DateTime.Now;

            var collection = new List<UserTestingClass>
                {
                    new UserTestingClass { FirstName = "Simeon", Id = userId, TimeGenerated = dateTimeNow }
                };

            Expression<Func<UserTestingClass, bool>> filterExpression = Core.Filters.FiltersExpressionBuilder.Create<UserTestingClass>(new List<Filter>
                {
                    new Filter
                        {
                        Operator = Operator.Equals, 
                        PropertyName = "Id",
                        Value = userId.ToString(),
                        OrFilters = new List<Filter> { 
                            new Filter
                            {
                                Operator = Operator.Equals,
                                PropertyName = "TimeGenerated.Day", 
                                Value = dateTimeNow.Day
                            },
                            new Filter
                            {
                                Operator = Operator.Equals,
                                PropertyName = "Id", 
                                Value = userId.ToString()
                            }
                        }
                    }
                });

            // Act
            var result = collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(1, result.Count());
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

        private class UserTestingClass
        {
            public Guid Id { get; set; }

            public string FirstName { get; set; }

            public DateTime TimeGenerated { get; set; }
        }
    }
}