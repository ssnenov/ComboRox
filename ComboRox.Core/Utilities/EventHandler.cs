using System;
using System.Runtime.CompilerServices;

namespace ComboRox.Core.Utilities
{
    public static class EventHandler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TriggerEvent<T1>(this Action<T1> @event, T1 param)
        {
            if (@event != null)
            {
                @event(param);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TriggerEvent<T1, T2>(this Action<T1, T2> @event, T1 param1, T2 param2)
        {
            if (@event != null)
            {
                @event(param1, param2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TriggerEvent<T1, T2, T3>(this Action<T1, T2, T3> @event, T1 param1, T2 param2, T3 param3)
        {
            if (@event != null)
            {
                @event(param1, param2, param3);
            }
        }
    }
}