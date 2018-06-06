namespace Colony.Model.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Colony.Model.BaseTypes;

    public enum Structure
    {
        
    }

    public class StructureInfo : DictionaryEntity
    {
        
        public StructureInfo(Guid id, string name) : base(id, name)
        {
        }

        private static readonly StructureInfo[] knownStructures = new[]
                {
                    new StructureInfo(new Guid(@"{AA448EE0-B784-4445-8A76-FDE58E6539C5}"), @"Power Plant")
                        {
                            Costs = new BuildingCost(
                                new ResourceRequirement(Resource.Silicon, 100),
                                new ResourceRequirement(Resource.Steel, 50),
                                new ResourceRequirement(Resource.Workload, 200), // required labour -> time
                                new ResourceRequirement(Resource.Power, 20) // required power for construction * time - deducted from monthly production
                                ),
                        },
                    new StructureInfo(new Guid(@"{958209FD-80DD-4284-A36D-D683353791C9}"), @"Mine"), // extracts raw resources
                    new StructureInfo(new Guid(@"{D530E829-EDAA-49FC-B832-768F84C78731}"), @"Farm"), // produces food
                    new StructureInfo(new Guid(@"{ED3AFD43-2D1C-4EBA-B720-349CB641A274}"), @"Storage"), // can store products
                    new StructureInfo(new Guid(@"{4074CD5F-C8B7-4E95-9B0E-D626AA11967C}"), @"Landing Pad"), // ships cvan land
                    new StructureInfo(new Guid(@"{8F49025E-E03F-4FF1-AF41-320D24EEC5F5}"), @"Quarters"), // people live there
                    new StructureInfo(new Guid(@"{E6233A47-A386-4BF5-9726-ACFC9B583256}"), @"Forge"), // converts Junk and Iron ore into steel
                    new StructureInfo(new Guid(@"{F3AD9A0C-00B5-4D76-923B-BFC6B13D16B8}"), @"Milling Plant"), // converts Uranium ore into Treated Uranium
                    new StructureInfo(new Guid(@"{4D2894AC-A596-48CD-B34B-21BB2EE3BE06}"), @"Robotics"), // produces robots
                };

        public BuildingCost Costs { get; set; }

        public static IEnumerable<StructureInfo> KnownStructures
        {
            get
            {
                return knownStructures.ToArray(); // clone?
            }
        }
    }
}