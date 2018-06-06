namespace Colony.Model.BaseTypes
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Index} - {Value}")]
    public struct IndexedValue<TIndex, TValue> : IEquatable<IndexedValue<TIndex, TValue>>
        where TIndex : IIndexer
        where TValue : IArithmetic<TValue>
    {
        public IndexedValue(TIndex index, TValue value)
        {
            this.Index = index;
            this.Value = value;
        }

        public TIndex Index { get; private set; }

        public TValue Value { get; private set; }

        public static IndexedValue<TIndex, TValue> operator +(IndexedValue<TIndex, TValue> r1, IndexedValue<TIndex, TValue> r2)
        {
            if (r1.Index.GetIndexer() != r2.Index.GetIndexer())
            {
                throw new InvalidOperationException("Cannot add Values with different indexing value");
            }

            return new IndexedValue<TIndex, TValue>(r1.Index, r1.Value.Add(r2.Value));
        }

        public static IndexedValue<TIndex, TValue> operator -(IndexedValue<TIndex, TValue> r1, IndexedValue<TIndex, TValue> r2)
        {
            if (r1.Index.GetIndexer() != r2.Index.GetIndexer())
            {
                throw new InvalidOperationException("Cannot subtract Values with different indexing value");
            }

            return new IndexedValue<TIndex, TValue>(r1.Index, r1.Value.Subtract(r2.Value));
        }

        public bool Equals(IndexedValue<TIndex, TValue> other)
        {
            if (object.Equals(this, other))
            {
                return true;
            }
            
            return this.Index.GetIndexer() == other.Index.GetIndexer()
                && this.Value.Equals(other.Value);
        }
    }
}