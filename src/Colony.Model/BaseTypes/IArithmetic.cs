namespace Colony.Model.BaseTypes
{
    using System;

    public interface IArithmetic<T> : IEquatable<T>, IComparable<T>
    {
        T Add(T other);

        T Subtract(T other);

        // TODO: more
    }
}