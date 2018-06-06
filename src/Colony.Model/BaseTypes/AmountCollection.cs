namespace Colony.Model.BaseTypes
{
    public class AmountCollection<TIndex> : IndexedCollection<TIndex, Amount>
        where TIndex : IIndexer
    {
    }
}