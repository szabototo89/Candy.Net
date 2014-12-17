using System;

namespace Candy.Core
{
    public interface IMonadic<out TValue>
    {
        IMonadic<TResult> Map<TResult>(Func<TValue, TResult> transform);

        IMonadic<TValue> Filter(Predicate<TValue> predicate);
    }
}