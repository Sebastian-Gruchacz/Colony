namespace Colony.Model.BaseTypes
{
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MaxIndexAttribute : Attribute
    {
        public MaxIndexAttribute(Type maxIndexEnumType)
        {
            if (!maxIndexEnumType.IsEnum)
            {
                throw new ArgumentException("Must be Enum type!", nameof(maxIndexEnumType));
            }
            if (Enum.GetValues(maxIndexEnumType).Cast<uint>().Any(v => v < 0))
            {
                throw new ArgumentException("Enum type must not use negative values!", nameof(maxIndexEnumType));
            }
            this.MaxIndexEnumType = maxIndexEnumType;
        }

        public Type MaxIndexEnumType { get; private set; }
    }
}