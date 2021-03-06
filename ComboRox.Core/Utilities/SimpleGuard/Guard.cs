﻿using System;

namespace ComboRox.Core.Utilities.SimpleGuard
{
    public abstract class Guard
    {
        public static RequirementBase<T> Requires<T>(T parameter, string parameterName)
        {
            return new RequirementBase<T>(parameter, parameterName);
        }

        public static RequirementBase<T> Requires<T>(Func<T> selector, string parameterName)
        {
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}

			if (string.IsNullOrEmpty(parameterName))
			{
				throw new ArgumentNullException("parameterName");
			}

            T value = selector.Invoke();

            return new RequirementBase<T>(value, parameterName);
        }

        public static void Requires(bool expression, string exceptionMessage)
        {
            if (!expression)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}