namespace Colony.Model.Core
{
    using System;

    public class ResourceAmount : IEquatable<ResourceAmount>
    {
        public ResourceAmount(ResourceInfo resource, uint amount)
        {
            Resource = resource;
            Amount = amount;
        }

        /// <summary>
        /// Points to a well known resource definition
        /// </summary>
        public ResourceInfo Resource { get; private set; }

        /// <summary>
        /// Quantity, amount of given resource
        /// </summary>
        public uint Amount { get; private set; }

        public static ResourceAmount operator +(ResourceAmount r1, ResourceAmount r2)
        {
            if (r1 == null) throw new ArgumentNullException(nameof(r1));
            if (r2 == null) throw new ArgumentNullException(nameof(r2));

            if (r1.Resource.ResourceType != r2.Resource.ResourceType)
            {
                throw new InvalidOperationException("Cannot add different resources");
            }

            return new ResourceAmount(r1.Resource, r1.Amount + r2.Amount);
        }

        public static ResourceAmount operator -(ResourceAmount r1, ResourceAmount r2)
        {
            if (r1 == null) throw new ArgumentNullException(nameof(r1));
            if (r2 == null) throw new ArgumentNullException(nameof(r2));

            if (r1.Resource.ResourceType != r2.Resource.ResourceType)
            {
                throw new InvalidOperationException("Cannot substract different resources");
            }

            return new ResourceAmount(r1.Resource, r1.Amount - r2.Amount);
        }

        public bool Equals(ResourceAmount other)
        {
            if (object.Equals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return this.Resource.ResourceType == other.Resource.ResourceType &&
                   this.Amount == other.Amount;
        }
    }
}