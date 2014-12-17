using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Linq
{
    public static class ObjectExtensions
    {
        public static TValue Safe<TValue>(this TValue value)
            where TValue : class, new()
        {
            if (value == null)
                return new TValue();

            return value;
        }

        public static TValue With<TValue>(this TValue value, Action<TValue> initializer)
        {
            if (initializer != null)
                initializer(value);

            return value;
        }
        public static TValue Init<TValue>(this TValue value, Action<TValue> initializer)
            where TValue : class, new()
        {
            return Safe(value).With(initializer);
        }
    }
}
