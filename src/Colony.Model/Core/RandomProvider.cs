namespace Colony.Model.Core
{
    using System;

    public class RandomProvider
    {
        Random rnd = new Random();

        public decimal NextDecimal(decimal minimum, decimal maximum)
        {
            return (decimal)this.rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        internal decimal NextDecimal()
        {
            return (decimal)this.rnd.NextDouble();
        }

        public int NextInt(int minimum, int maximum)
        {
            return this.rnd.Next(minimum, maximum);
        }

        public int NextInt()
        {
            return this.rnd.Next();
        }
    }
}