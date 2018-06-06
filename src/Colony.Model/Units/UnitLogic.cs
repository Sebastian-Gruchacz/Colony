namespace Colony.Model.Core
{
    using System;
    using System.Linq;

    using Colony.Model.Fight;
    using Colony.Model.Units;

    public class UnitLogic
    {
        private readonly GameState gameState;

        private readonly RandomProvider rnd;

        public UnitLogic(GameState gameState, RandomProvider rnd)
        {
            this.gameState = gameState;
            this.rnd = rnd;
        }

        public decimal GetMilitaryPower(UnitCollection units, MilitarySide side)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));

            switch (side)
            {
                case MilitarySide.Ofense:
                    return units.GetAll().Sum(u => (u.Amount * (u.Unit.Ofense)));
                case MilitarySide.Defense:
                    return units.GetAll().Sum(u => (u.Amount * (u.Unit.Defense)));
                case MilitarySide.Both:
                    return units.GetAll().Sum(u => (u.Amount * (u.Unit.Ofense + u.Unit.Defense)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
            
        }

        public decimal GetSciencePower(UnitCollection units)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));

            return units.GetAll().Sum(u => (u.Amount * u.Unit.Science));
        }

        public decimal GetLabourPower(UnitCollection units)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));

            return units.GetAll().Sum(u => (u.Amount * u.Unit.Workload));
        }

        public void AddWorkers(UnitCollection units, uint amount)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));

            units.Add(new UnitAmount(UnitInfo.WorkerUnitType, amount));
        }

        public UnitCollection GenerateRandomForces(decimal dangerLevel)
        {
            if (dangerLevel > 0.0m)
            {
                var player = this.GetRandomPlayer(PlayerType.Enemy); // TODO: pass danger level?
                if (player == null)
                {
                    return UnitCollection.Empty;
                }

                var units = player.BaseUnits.GetAll();
                // TODO: more complex calculations, than simple danger, percentage 
                return new UnitCollection(units.Select(u => new UnitAmount(u.Unit, (uint)(u.Amount * dangerLevel))).ToArray());

                // TODO: find a way to retroactively reduce forces after fight...
            }
            else
            {
                return UnitCollection.Empty;
            }
        }

        private Player GetRandomPlayer(PlayerType playerType)
        {
            var enemies = this.gameState.Players.Where(p => p.PlayerType == playerType).ToArray();
            if (!enemies.Any()) return null;

            return enemies[this.rnd.NextInt(0, enemies.Length)];
        }
    }
}