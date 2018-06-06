namespace Colony.Model.Core
{
    using System.Collections.Generic;

    using Colony.Model.Printers;

    [Printer(typeof(ResourceCollectionPrinter))]
    public class ResourceCollection
    {
        readonly Dictionary<ResourceInfo, uint> resources = new Dictionary<ResourceInfo, uint>();

        public void Add(ResourceAmount resourceAmount)
        {
            if (this.resources.ContainsKey(resourceAmount.Resource))
            {
                uint currentAmount = this.resources[resourceAmount.Resource];
                
                this.resources[resourceAmount.Resource] = currentAmount + resourceAmount.Amount;
            }
            else
            {
                this.resources.Add(resourceAmount.Resource, resourceAmount.Amount);
            }
        }

        public IEnumerable<ResourceAmount> GetAll()
        {
            foreach (KeyValuePair<ResourceInfo, uint> kvp in this.resources)
            {
                yield return new ResourceAmount(kvp.Key, kvp.Value);
            }
        }
    }
}