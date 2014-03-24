using System;
using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ComboRox.Core.UnitTests.Filters.FiltersExpressionFactory
{
    [TestClass]
    public class FiltersExpressionFactory_CreateFilterExpression_Should : FiltersExpressionFactoryTestsBase
    {
        [TestMethod]
        public void ReturnZeroItems_AfterFiltering()
        {
            // Arrange
            Expression<Func<UserTestingClass, bool>> filterExpression = Core.Filters.FiltersExpressionFactory.Create<UserTestingClass>(new List<Filter>
                {
                    new Filter { Operator = Operator.Equals, PropertyName = "FirstName", Value = "John" },
                    new Filter { Operator = Operator.Equals, PropertyName = "LastName", Value = "Brown" }
                });

            // Act
            var result = Collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ReturnCorrectExpression_WhenPassingOrExpresionFilters()
        {
            // Arrange
            Expression<Func<UserTestingClass, bool>> filterExpression = Core.Filters.FiltersExpressionFactory.Create<UserTestingClass>(new List<Filter>
                {
                    new Filter
                        {
                            Operator = Operator.Equals, 
                            PropertyName = "FirstName",
                            Value = "John",
                            OrFilters = new List<Filter> { new Filter { Operator = Operator.Equals, PropertyName = "LastName", Value = "Brown" } }
                        }
                });

            // Act
            var result = Collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ReturnCorrectExpression_WhenPassingOrExpresionFiltersAndPropertiesToParse()
        {
            // Arrange
            DateTime dateTimeNow = DateTime.Now;
            UserTestingClass firstEntry = Collection.First();
            firstEntry.TimeGenerated = dateTimeNow;

            Expression<Func<UserTestingClass, bool>> filterExpression = Core.Filters.FiltersExpressionFactory.Create<UserTestingClass>(new List<Filter>
                {
                    new Filter
                        {
                            Operator = Operator.Equals, 
                            PropertyName = "Id",
                            Value = Guid.NewGuid().ToString(), // Fake ID
                            OrFilters = new List<Filter> 
                            { 
                                new Filter
                                {
                                    Operator = Operator.Equals,
                                    PropertyName = "TimeGenerated.Day", 
                                    Value = dateTimeNow.Day.ToString()
                                },
                                new Filter
                                {
                                    Operator = Operator.Equals,
                                    PropertyName = "TimeGenerated.Minute", 
                                    Value = dateTimeNow.Minute.ToString()
                                }
                            }
                    }
                });

            // Act
            var result = this.Collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ReturnCorrectExpression_WhenPassingEqualsNotEqualsExpresionFilters()
        {
            // Arrange
            Expression<Func<UserTestingClass, bool>> filterExpression = Core.Filters.FiltersExpressionFactory.Create<UserTestingClass>(new List<Filter>
                {
                    new Filter
                        {
                        Operator = Operator.Equals, 
                        PropertyName = "FirstName",
                        Value = "Peter"
                    },
                    new Filter 
                    { 
                        Operator = Operator.NotEquals,
                        PropertyName = "LastName",
                        Value = "Doe" 
                    }
                });

            // Act
            var result = Collection.AsQueryable().Where(filterExpression);

            // Assert
            Assert.AreEqual(1, result.Count());
        }
    }
}