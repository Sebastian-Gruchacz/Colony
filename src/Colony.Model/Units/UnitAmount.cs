namespace Colony.Model.Core
{
    using System;

    using Colony.Model.Units;

    public class UnitAmount
    {
        public UnitAmount(UnitInfo unitType, uint amount)
        {
            if (unitType == null) throw new ArgumentNullException(nameof(unitType));

            Amount = amount;
            Unit = unitType;
        }

        public UnitInfo Unit { get; private set; }

        public uint Amount { get; private set; }
    }
}