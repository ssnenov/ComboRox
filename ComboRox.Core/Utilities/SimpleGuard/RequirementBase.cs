using System;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    public class RequirementBase<T>
    {
        public RequirementBase(T parameterValue, string parameterName)
        {
            this.ParameterValue = parameterValue;
            this.ParameterName = parameterName;
        }

		protected internal T ParameterValue { get; private set; }

		protected internal string ParameterName { get; private set; }

        public RequirementBase<T> IsNotNull()
        {
            if (this.ParameterValue == null)
            {
                throw new ArgumentNullException(this.ParameterName);
            }

            return this;
        }
    }
}