using System;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    public static class GuidRequirement
    {
        public static RequirementBase<Guid> IsNotNullOrEmpty(this RequirementBase<Guid> requirement)
        {
            if (requirement == null)
            {
                throw new ArgumentException("requirement");
            }

            if (requirement.ParameterValue == Guid.Empty)
            {
                throw new ArgumentException(requirement.ParameterName);
            }

            return requirement;
        }
    }
}