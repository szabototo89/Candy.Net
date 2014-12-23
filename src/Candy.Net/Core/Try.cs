using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Core
{
    public abstract class Try<TValue>
    {
        public abstract TValue Get();

        public abstract TValue GetOrDefault(TValue defaultValue = default(TValue));
    }

    public static class Try
    {
        public static Try<TResult> Select<TValue, TResult>(this Try<TValue> value, Func<TValue, TResult> function)
        {
            if (value is Failure<TValue>) {
                var failure = (Failure<TValue>)value;
                return new Failure<TResult>(failure.Exception);
            }

            return new Success<TResult>(function(value.Get()));
        }

        public static Try<TValue> Success<TValue>(TValue value)
        {
            return new Success<TValue>(value);
        }

        public static Try<TValue> Failure<TValue>(Exception exception)
        {
            return new Failure<TValue>(exception);
        }
    }

    public sealed class Success<TValue> : Try<TValue>, IEnumerable<TValue>
    {
        public TValue Value { get; private set; }

        /// <summary>
        /// Default constructor of Success
        /// </summary>
        /// <param name="value"></param>
        internal Success(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            yield return Get();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Get();
        }

        public override TValue Get()
        {
            return Value;
        }

        public override TValue GetOrDefault(TValue defaultValue = default(TValue))
        {
            return Get();
        }
    }

    public sealed class Failure<TValue> : Try<TValue>, IEnumerable<TValue>
    {
        public Exception Exception { get; private set; }

        /// <summary>
        /// Default constructor of Failure
        /// </summary>
        /// <param name="exception"></param>
        internal Failure(Exception exception)
        {
            Exception = exception;
        }

        public override TValue Get()
        {
            throw Exception;
        }

        public override TValue GetOrDefault(TValue defaultValue = default(TValue))
        {
            return defaultValue;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return Enumerable.Empty<TValue>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<TValue>().GetEnumerator();
        }
    }
}
