using System.Collections.Generic;
using ComboRox.Models.JsonObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComboRox.Core.UnitTests.Filters.FiltersExpressionBuilder
{
    public class FiltersExpressionBuilderTestsBase
    {
        public List<FilterObject> collection;

        [TestInitialize]
        public void InitializeData()
        {
            this.collection = new List<FilterObject>()
                {
                    new FilterObject
                        {
                            op = "eq",
                            prop = "FirstName",
                            value = "test"
                        },
                    new FilterObject
                        {
                            op = "eq",
                            prop = "LastName",
                            value = "test"
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