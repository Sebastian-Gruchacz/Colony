namespace Colony.Model
{
    public class ResourceRequirement
    {
        public Resource ResourceType { get; private set; }
        public int Amount { get; private set; }

        public ResourceRequirement(Resource resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}