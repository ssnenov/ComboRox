using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.Enums;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Filters
{
    public class FiltersFactory : IComponentFactory<Filter, FilterObject>
    {
        public List<Filter> Create(IEnumerable<FilterObject> componentObjects)
        {
            if (componentObjects == null)
            {
                return new List<Filter>();
            }

            return ExtractFilters(componentObjects);
        }

        private static List<Filter> ExtractFilters(IEnumerable<FilterObject> filterObjects)
        {
            return filterObjects.Select(filter => new Filter
                {
                    Operator = ExtractOperator(filter.Op),
                    PropertyName = filter.Prop,
                    Value = filter.Value,
                    OrFilters = filter.Or != null ? ExtractFilters(filter.Or) : null
                })
				.ToList();
        }

        private static Operator ExtractOperator(string op)
        {
            switch (op)
            {
                case "neq":
                    return Operator.NotEquals;
                case "lt":
                    return Operator.LessThan;
                case "gt":
                    return Operator.GreaterThan;
                case "cnt":
                    return Operator.Contains;
                default:
                    return Operator.Equals;
            }
        }
    }
}