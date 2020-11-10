using System;

namespace CSharp10
{
    // port from Nullable<int>
    // but this cannot get the special features of framework like Nullable<int>. (ex. boxing/unboxing)
    public struct NullableInt
    {
        private readonly int _value;
        private readonly bool _hasValue;

        public int Value
        {
            get
            {
                if (_hasValue)
                    throw new InvalidOperationException("Value is null.");
                return _value;
            }
        }
        public bool HasValue { get { return _hasValue; } }

        public NullableInt(int value)
        {
            _value = value;
            _hasValue = true;
        }

        public int GetValueOrDefault()
        {
            return _value;
        }

        public int GetValueOrDefault(int defalutValue)
        {
            return _hasValue ? _value : defalutValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return !_hasValue;

            Type objType = obj.GetType();
            if (objType == Type.GetType("System.Int32"))
                return Equals((int)obj);
            if (objType == GetType())
                return Equals((NullableInt)obj);

            return false;
        }

        public bool Equals(int other)
        {
            return _hasValue && _value == other;
        }

        public bool Equals(NullableInt other)
        {
            if (!_hasValue && !other.HasValue)
                return true;
            return _value == other.GetValueOrDefault();
        }

        public override int GetHashCode()
        {
            return _hasValue ? _value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return _hasValue ? _value.ToString() : "";
        }

        public static bool operator ==(NullableInt left, NullableInt right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NullableInt left, NullableInt right)
        {
            return !(left == right);
        }

        public static explicit operator int (NullableInt value)
        {
            return value.Value;
        }

        public static implicit operator NullableInt (int value)
        {
            return new NullableInt(value);
        }
    }
}