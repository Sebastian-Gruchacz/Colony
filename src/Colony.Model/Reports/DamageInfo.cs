namespace Colony.Model.Reports
{
    using Colony.Model.BaseTypes;

    public class DamageInfo<T> where T : DictionaryEntity
    {
        public T Entity { get; set; }

        public uint Quantity { get; set; }
    }
}