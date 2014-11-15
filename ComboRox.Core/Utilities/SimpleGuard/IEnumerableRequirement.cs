using System;
using System.Collections.Generic;
using System.Linq;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    public static class IEnumerableRequirement
    {
        public static RequirementBase<IEnumerable<TType>> IsNotEmpty<TType>(this RequirementBase<IEnumerable<TType>> requirement)
        {
			if (requirement == null)
			{
				throw new ArgumentNullException("requirement");
			}

            if (!requirement.ParameterValue.Any())
            {
                throw new Exception("Sequence contains no elements");
            }

            return requirement;
        }
    }
}