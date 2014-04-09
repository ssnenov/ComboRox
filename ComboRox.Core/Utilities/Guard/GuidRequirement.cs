using System;

namespace ComboRox.Core.Utilities.Guard
{
    public static class GuidRequirement
    {
        public static RequirementBase<Guid> IsNotNullOrEmpty(this RequirementBase<Guid> requirement)
        {
            if (requirement.ParameterValue == Guid.Empty)
            {
                throw new ArgumentException(requirement.ParameterName);
            }

            return requirement;
        }
    }
}