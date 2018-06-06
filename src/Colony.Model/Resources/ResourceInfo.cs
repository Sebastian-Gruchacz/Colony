namespace Colony.Model.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Colony.Model.BaseTypes;
    using Colony.Model.Units;

    /// <summary>
    /// Represents some game date about resources - localization + game data
    /// </summary>
    [MaxIndex(typeof(Resource))]
    [DebuggerDisplay("Resource-{Name}")]
    public class ResourceInfo : DictionaryEntity, IIndexer
    {
        public uint GetIndexer()
        {
            return (uint)this.ResourceType;
        }

        public Resource ResourceType { get; private set; }

        public BuyCost BaseBuyCost { get; private set; }

        public decimal FindingProbability { get; private set; }

        public decimal BaseFindSize { get; private set; }

        private ResourceInfo(Guid id, string name, Resource resourceType) : base (id, name)
        {
            this.ResourceType = resourceType;
        }

        private static readonly ResourceInfo _junk = new ResourceInfo(new Guid(@"{6294CB89-2115-4AD5-A2E7-6FE03EC8A723}"), @"Junk", Resource.Junk)
        {
            FindingProbability = 0.2m, // iron / steel junk can be sometimes found - more like artifacts or lost parts...
            BaseFindSize = 0.1m, // then it can be smelted...
            BaseBuyCost = new BuyCost(5)
        };

        private static readonly ResourceInfo[] knownResources = new[]
        {
            new ResourceInfo(new Guid(@"{1170139B-C3B5-4B0A-A5E8-510559DC0290}"), @"Silicon", Resource.Silicon)
            {
                FindingProbability = 0.5m,
                BaseFindSize = 10.0m, // shall be then multiplied by Expedition power and planet / player location parameters
                BaseBuyCost = new BuyCost(5)
            },
            new ResourceInfo(new Guid(@"{CBBA486B-682F-42A0-BF9F-ACBCA08452A4}"), @"Iron Ore", Resource.IronOre)
            {
                FindingProbability = 0.3m,
                BaseFindSize = 5.0m,
                BaseBuyCost = new BuyCost(15)
            },
            new ResourceInfo(new Guid(@"{308576E0-B5EE-4191-B8BE-0527CAD62DA2}"), @"Uranium Ore", Resource.UraniumOre)
            {
                FindingProbability = 0.1m,
                BaseFindSize = 1.0m,
                BaseBuyCost = new BuyCost(50)
            },
            new ResourceInfo(new Guid(@"{77907547-251F-48A2-BE50-06FFF509BF54}"), @"Food", Resource.Food)
            {
                FindingProbability = 0.0m, // Food cannot be found! Must be produced
                BaseFindSize = 0.0m,
                BaseBuyCost = new BuyCost(10)
            },
            new ResourceInfo(new Guid(@"{724C6D74-6590-4CC6-B9F3-7DF9E4EE199C}"), @"Power", Resource.Power)
            {
                FindingProbability = 0.0m, // Power must ber produced
                BaseFindSize = 0.0m,
                BaseBuyCost = BuyCost.NotAvailable // power cannot be bought - must be produced
            },
            new ResourceInfo(new Guid(@"{FDDD75A3-45FA-4228-9E0B-A06EC4A9EC73}"), @"Steel", Resource.Steel)
            {
                FindingProbability = 0.00m, // must be produced or recovered
                BaseFindSize = 0.0m,
                BaseBuyCost = new BuyCost(30)
            },
            new ResourceInfo(new Guid(@"{6294CB89-2115-4AD5-A2E7-6FE03EC8A723}"), @"Workload", Resource.Workload)
            {
                FindingProbability = 0.0m,
                BaseFindSize = 0.0m,
                BaseBuyCost = BuyCost.NotAvailable // cannot buy workload directly
            },
            new ResourceInfo(new Guid(@"{6294CB89-2115-4AD5-A2E7-6FE03EC8A723}"), @"Treated Uranium", Resource.TreatedUranium)
            {
                FindingProbability = 0.0m, // clean uranium cannot be found
                BaseFindSize = 0.0m, // clean uranium cannot be found
                BaseBuyCost = new BuyCost(100)
            },
            Junk
        };

        public static IEnumerable<ResourceInfo> KnownResources
        {
            get
            {
                return knownResources.ToArray(); // clone?
            }
        }

        public static ResourceInfo Junk
        {
            get { return _junk; }
        }
    }
}