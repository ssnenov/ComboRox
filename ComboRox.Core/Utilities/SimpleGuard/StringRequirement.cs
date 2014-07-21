using System;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    public static class StringRequirement
    {
        public static RequirementBase<string> IsNotNullOrEmpty(this RequirementBase<string> requirement)
        {
            if (requirement == null)
            {
                throw new ArgumentNullException("requirement");
            }

            if (string.IsNullOrEmpty(requirement.ParameterValue))
            {
                throw new ArgumentNullException(requirement.ParameterName);
            }

            return requirement;
        }

        public static RequirementBase<string> IsNotNullOrWhiteSpace(this RequirementBase<string> requirement)
        {
            if (requirement == null)
            {
                throw new ArgumentNullException("requirement");
            }

            if (string.IsNullOrWhiteSpace(requirement.ParameterValue))
            {
                throw new ArgumentNullException(requirement.ParameterName);
            }

            return requirement;
        }
    }
}