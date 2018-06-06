namespace Colony.Model.BaseTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexedCollection<TIndex, TValue>
        where TValue : IArithmetic<TValue>
        where TIndex : IIndexer
    {
        #region Static Type initialization

        public static long MaxIndex { get; set; }

        static IndexedCollection()
        {
            Type t = typeof(TIndex);
            var indexerAttribute = t.GetAttribute<MaxIndexAttribute>();
            if (indexerAttribute == null)
            {
                throw new NotImplementedException($"Indexing type must be decorated with {nameof(MaxIndexAttribute)} attribute.");
            }
            Type maxIndexEnum = indexerAttribute.MaxIndexEnumType;
            MaxIndex = Enum.GetValues(maxIndexEnum).Cast<uint>().Max() + 1; // array indexing...
        }

        #endregion Static Type initialization

        private readonly TValue[] values;

        private readonly TIndex[] indexes;

        public IndexedCollection()
        {
            this.values = new TValue[MaxIndex];
            this.indexes = new TIndex[MaxIndex];
        }

        public IndexedCollection(params IndexedValue<TIndex, TValue>[] indexedValues) : this()
        {
            foreach (var indexedValue in indexedValues)
            {
                this.values[indexedValue.Index.GetIndexer()] = this.values[indexedValue.Index.GetIndexer()].Add(indexedValue.Value);
            }
        }

        public IEnumerable<IndexedValue<TIndex, TValue>> GetAll()
        {
            for (int i = 0; i < MaxIndex; i++)
            {
                if (this.indexes[i] == null)
                    continue; // not set

                yield return new IndexedValue<TIndex, TValue>(this.indexes[i], this.values[i]);
            }
        }

        public void Subtract(IndexedCollection<TIndex, TValue> otherCollection)
        {
            for (int i = 0; i < MaxIndex; i++)
            {
                if (otherCollection.indexes[i] == null)
                    continue; // not set

                if (this.indexes[i] == null)
                    this.indexes[i] = otherCollection.indexes[i];

                this.values[i] = this.values[i].Subtract(otherCollection.values[i]);
            }
        }

        public void Add(IndexedCollection<TIndex, TValue> otherCollection)
        {
            for (int i = 0; i < MaxIndex; i++)
            {
                if (otherCollection.indexes[i] == null)
                    continue; // not set

                if (this.indexes[i] == null)
                    this.indexes[i] = otherCollection.indexes[i];

                this.values[i] = this.values[i].Add(otherCollection.values[i]);
            }
        }


        public void Add(IndexedValue<TIndex, TValue> indexedValue)
        {
            uint index = indexedValue.Index.GetIndexer();
            if (this.indexes[index] == null)
                this.indexes[index] = indexedValue.Index;

            this.values[index] = this.values[index].Add(indexedValue.Value);
        }

        public void Subtract(IndexedValue<TIndex, TValue> indexedValue)
        {
            uint index = indexedValue.Index.GetIndexer();
            if (this.indexes[index] == null)
                this.indexes[index] = indexedValue.Index;

            this.values[index] = this.values[index].Subtract(indexedValue.Value);
        }

        public TValue this[TIndex index]
        {
            get
            {
                if (index == null)
                    throw new ArgumentNullException(nameof(index));

                return this.values[index.GetIndexer()];
            }
            set
            {
                if (index == null)
                    throw new ArgumentNullException(nameof(index));

                this.values[index.GetIndexer()] = value;
            }
        }









        public IndexedCollection<TIndex, TValue> Clone()
        {
            return new IndexedCollection<TIndex, TValue>(this.GetAll().ToArray());
        }

        public static IndexedCollection<TIndex, TValue> Empty
        {
            get
            {
                return new IndexedCollection<TIndex, TValue>();
            }
        }
    }
}