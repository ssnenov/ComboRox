using System;

namespace ComboRox.Core.Utilities.Guard
{
    public abstract class Guard
    {
        public static RequirementBase<T> Requires<T>(T parameter, string parameterName)
        {
            return new RequirementBase<T>(parameter, parameterName);
        }

        public static RequirementBase<T> Requires<T>(Func<T> selector, string parameterName)
        {
            T value = selector.Invoke();

            return new RequirementBase<T>(value, parameterName);
        }

        public static void Requires(bool expression, string exceptionMessage)
        {
            if (!expression)
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}