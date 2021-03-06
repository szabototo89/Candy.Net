﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Core
{
    public static class Option
    {
        public static Option<TValue> Some<TValue>(TValue value)
        {
            return new Option<TValue>(value);
        }

        public static readonly None None = None.Instance;
    }


    /// <summary>
    /// General type of Option(T)
    /// </summary>
    /// <typeparam name="TValue">Type of value</typeparam>
    public struct Option<TValue> : IMonadic<TValue>, IEquatable<Option<TValue>>
    {
        private Boolean _isNone;

        private TValue _value;
        public Boolean HasValue { get; private set; }

        public TValue Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException();
                return _value;
            }
            private set { _value = value; }
        }

        public Option(TValue value)
            : this()
        {
            Value = value;
            HasValue = true;
        }

        public static implicit operator Option<TValue>(None value)
        {
            return new Option<TValue>() { _isNone = true };
        }

        public static implicit operator TValue(Option<TValue> value)
        {
            if (!value.HasValue)
                return default(TValue);

            return value.Value;
        }

        public static Boolean operator ==(Option<TValue> left, Option<TValue> right)
        {
            if (left._isNone != right._isNone)
                return false;

            if (left.HasValue != right.HasValue)
                return false;

            if (!left.HasValue)
                return true;

            return Equals(left.Value, right.Value);
        }

        public static Boolean operator !=(Option<TValue> left, Option<TValue> right)
        {
            return !(left == right);
        }

        public Boolean Equals(Option<TValue> other)
        {
            if (_isNone && other._isNone)
                return true;

            return HasValue.Equals(other.HasValue) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option<TValue> && Equals((Option<TValue>)obj);
        }

        public override Int32 GetHashCode()
        {
            unchecked {
                return (HasValue.GetHashCode() * 397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }

        public Option<TResult> Map<TResult>(Func<TValue, TResult> transform)
        {
            if (!HasValue)
                return Option.None;

            return Option.Some(transform(this.Value));
        }

        public Option<TValue> Filter(Predicate<TValue> predicate)
        {
            if (HasValue && predicate(Value))
                return this;

            return Option.None;
        }

        IMonadic<TResult> IMonadic<TValue>.Map<TResult>(Func<TValue, TResult> transform)
        {
            return Map(transform);
        }

        IMonadic<TValue> IMonadic<TValue>.Filter(Predicate<TValue> predicate)
        {
            return Filter(predicate);
        }
    }

    public sealed class None
    {
        internal static readonly None Instance = new None();
        private None() { }
    }
}
