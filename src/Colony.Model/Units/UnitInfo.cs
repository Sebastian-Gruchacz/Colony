namespace Colony.Model.Units
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Colony.Model.BaseTypes;

    [DebuggerDisplay("UnitInfo-{Name}")]
    public class UnitInfo : DictionaryEntity
    {
        public UnitType UnitType { get; private set; }

        public decimal Defense { get; private set; }

        public decimal Ofense { get; private set; }

        public uint Science { get; private set; }

        public uint Workload { get; private set; }

        public ProductionCosts ProductionCosts { get; private set; }

        public BuyCost BuyBaseCosts { get; private set; }

        private UnitInfo(Guid id, string name, UnitType unitType) : base (id, name)
        {
            this.UnitType = unitType;
        }
        

        private static readonly UnitInfo[] knownUnits = new[]
                   {
                    WorkerUnitType,
                    SoldierUnitType,
                    ScientistUnitType,
                    WargUnitType
                    // ...
                };

        private static readonly UnitInfo _workerUnit = new UnitInfo(new Guid(@"{D10BEE11-4EA8-4F8C-818E-DDBB065F6066}"), @"Worker", UnitType.Human)
            {
                Defense = 1,
                Ofense = 2,
                Workload = 10,
                Science = 1,
                ProductionCosts = ProductionCosts.NotAvailable,
                BuyBaseCosts = new BuyCost(100)
            };

        private static readonly UnitInfo _soldierUnit = new UnitInfo(new Guid(@"{633BB8A1-F9C1-439F-81AE-3F7BF2D0FD0E}"), @"Soldier", UnitType.Human)
            {
                Defense = 10,
                Ofense = 10,
                Workload = 5,
                Science = 0,
                ProductionCosts = ProductionCosts.NotAvailable,
                BuyBaseCosts = new BuyCost(150)
            };

        private static readonly UnitInfo _scientistUnit = new UnitInfo(new Guid(@"{8C29E4C5-C916-4B81-8CFF-E9C72393FEDC}"), @"Scientist", UnitType.Human)
            {
                Defense = 1,
                Ofense = 3,
                Workload = 1,
                Science = 10,
                ProductionCosts = ProductionCosts.NotAvailable,
                BuyBaseCosts = new BuyCost(200)
            };

        private static readonly UnitInfo _wargUnit = new UnitInfo(new Guid(@"{E60BCB13-66BD-413D-B7B3-61157C883366}"), @"Warg-15", UnitType.Alien)
            {
                Defense = 15,
                Ofense = 15,
                Workload = 0,
                Science = 0,
                ProductionCosts = ProductionCosts.NotAvailable,
                BuyBaseCosts = BuyCost.NotAvailable
            };

    public static IEnumerable<UnitInfo> KnownUnits
        {
            get
            {
                return knownUnits.ToArray(); // clone?
            }
        }

        public static UnitInfo WorkerUnitType { get { return _workerUnit; } }

        public static UnitInfo SoldierUnitType { get { return _soldierUnit; } }

        public static UnitInfo ScientistUnitType { get { return _scientistUnit; } }

        public static UnitInfo WargUnitType { get { return _wargUnit; } }
    }

    public class BuyCost
    {
        private int v;

        public BuyCost(int v)
        {
            this.v = v;
            this.CanPlayerBuy = true;
        }

        public static BuyCost NotAvailable
        {
            get
            {
                return new BuyCost(-1)
                {
                    CanPlayerBuy = false
                };
            }
        }

        public bool CanPlayerBuy { get; private set; }
    }

    public class ProductionCosts
    {
        public ProductionCosts()
        {
            this.CanPlayerProduce = true;
        }

        public bool CanPlayerProduce { get; private set; }


        public static ProductionCosts NotAvailable
        {
            get
            {
                return new ProductionCosts
                {
                    CanPlayerProduce = false
                };
            }
        }
    }
}