namespace Colony.Model
{
    using System;

    public abstract class DictionaryEntity
    {
        protected DictionaryEntity(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}