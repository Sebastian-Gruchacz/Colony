namespace Colony.Model.Core
{
    using System;

    public class UnitAmount
    {
        public UnitAmount(Unit unitType, uint amount)
        {
            if (unitType == null) throw new ArgumentNullException(nameof(unitType));

            Amount = amount;
            Unit = unitType;
        }

        public Unit Unit { get; private set; }

        public uint Amount { get; private set; }
    }
}