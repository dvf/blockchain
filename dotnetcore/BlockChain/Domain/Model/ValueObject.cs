using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace BlockChain.Domain.Model
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private readonly BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public bool Equals(ValueObject other)
        {
            if (other == null)
                return false;

            Type type = GetType();
            Type otherType = other.GetType();

            if (type != otherType)
                return false;

            return type.GetFields(flags)
                .Select(field => new
                {
                    thisValue = field.GetValue(this),
                    otherValue = field.GetValue(other)
                })
                .All(field => object.Equals(field.thisValue, field.otherValue));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as ValueObject;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            int hashCode = 17;
            int multiplier = 59;

            var type = GetType();
            type.GetFields(flags)
                .Select(field => field.GetValue(this))
                .Where(value => value != null)
                .ToList()
                .ForEach(value => {
                    hashCode = hashCode * multiplier + value.GetHashCode();
                });

            return hashCode;
        }

        public override string ToString()
        {
            var type = GetType();
            return type.GetProperties(flags)
                .Select(prop => new {
                    Name = prop.Name, 
                    Value = prop.GetValue(this)
                })
                .ToList()
                .AsJson();
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            return object.Equals(a,b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

    }
}