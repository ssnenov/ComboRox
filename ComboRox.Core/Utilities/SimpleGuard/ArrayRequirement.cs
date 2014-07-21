using System;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    internal static class ArrayRequirement
    {
        public static RequirementBase<TType[]> IsNotEmpty<TType>(this RequirementBase<TType[]> requirement)
        {
            if (requirement.ParameterValue.Length == 0)
            {
                throw new ArgumentException(string.Format("Sequence \"{0}\" contains no elements", requirement.ParameterName));
            }

            return requirement;
        }
    }
}