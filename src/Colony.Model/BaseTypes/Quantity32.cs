namespace Colony.Model.BaseTypes
{
    using System;

    /// <summary>
    /// Represents quantifiable, non-negative value
    /// </summary>
    public struct Quantity32 : IArithmetic<Quantity32>
    {
        public Quantity32(uint value)
        {
            this.Value = value;
        }

        public Quantity32(Quantity32 value)
        {
            this.Value = value.Value;
        }

        public UInt32 Value { get; private set; }

        public Quantity32 Add(Quantity32 other)
        {
            return new Quantity32(this.Value + other.Value);
        }

        public Quantity32 Subtract(Quantity32 other)
        {
            if (this.Value > other.Value)
            {
                return new Quantity32(this.Value - other.Value);
            }
            else
            {
                return new Quantity32((uint)0);
            }
        }
        
        public bool Equals(Quantity32 other)
        {
            return this.Value.Equals(other.Value);
        }

        public int CompareTo(Quantity32 other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public static implicit operator uint(Quantity32 q)
        {
            return q.Value;
        }

        public static implicit operator Quantity32(uint i)
        {
            return new Quantity32(i);
        }
    }
}