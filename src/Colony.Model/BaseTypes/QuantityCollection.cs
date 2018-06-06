namespace Colony.Model.BaseTypes
{
    public class QuantityCollection<TIndex> : IndexedCollection<TIndex, Quantity32>
        where TIndex : IIndexer
    {
    }
}
