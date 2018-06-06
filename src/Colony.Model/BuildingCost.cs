namespace Colony.Model
{
    public class BuildingCost
    {
        public ResourceRequirement[] RequiredResources { get; private set; }

        public BuildingCost(params ResourceRequirement[] resourceRequirements)
        {
            this.RequiredResources = resourceRequirements;
        }
    }
}