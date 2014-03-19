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
                            Op = "eq",
                            Prop = "FirstName",
                            Value = "test"
                        },
                    new FilterObject
                        {
                            Op = "eq",
                            Prop = "LastName",
                            Value = "test"
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