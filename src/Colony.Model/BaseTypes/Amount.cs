namespace Colony.Model.BaseTypes
{
    using System;

    /// <summary>
    /// Represents non-integer amount or quantity
    /// </summary>
    public struct Amount : IArithmetic<Amount>
    {
        public Amount(Decimal value)
        {
            this.Value = value;
        }

        public Amount(Amount value)
        {
            this.Value = value.Value;
        }

        public Decimal Value { get; private set; }

        public Amount Add(Amount other)
        {
            return new Amount(this.Value + other.Value);
        }

        public Amount Subtract(Amount other)
        {
            return new Amount(this.Value - other.Value);
        }

        public bool Equals(Amount other)
        {
            return this.Value.Equals(other.Value);
        }

        public static implicit operator decimal (Amount a)
        {
            return a.Value;
        }
        
        public static implicit operator Amount(decimal i)
        {
            return new Amount(i);
        }

        public int CompareTo(Amount other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}