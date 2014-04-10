using System;

namespace ComboRox.Core.Utilities.Guard
{
    public class RequirementBase<T>
    {
        protected internal readonly T ParameterValue;

        protected internal readonly string ParameterName;

        public RequirementBase(T parameterValue, string parameterName)
        {
            this.ParameterValue = parameterValue;
            this.ParameterName = parameterName;
        }

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