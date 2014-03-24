using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComboRox.Core.UnitTests.Filters.FiltersExpressionFactory
{
    public class FiltersExpressionFactoryTestsBase
    {
        private List<UserTestingClass> collection;

        public List<UserTestingClass> Collection
        {
            get { return this.collection; }
        }

        [TestInitialize]
        public void InitializeData()
        {
            this.collection = new List<UserTestingClass>()
                {
                    new UserTestingClass
                        {
                            Id = new Guid(),
                            FirstName = "John",
                            LastName = "Doe",
                            Age = 42
                        },
                    new UserTestingClass
                        {
                            Id = new Guid(),
                            FirstName = "Peter",
                            LastName = "Brown",
                            Age = 27
                        }
                };
        }

        [TestCleanup]
        public void ClearData()
        {
            this.collection.Clear();
        }
    }
}