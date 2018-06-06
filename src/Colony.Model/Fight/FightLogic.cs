using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Hosting;
using Colony.Model.Core;

namespace Colony.Model.Fight
{
    using Colony.Model.BaseTypes;
    using Colony.Model.Resources;

    public class FightLogic
    {
        private readonly RandomProvider _rnd;
        private readonly UnitLogic unitLogic;

        public FightLogic(RandomProvider rnd, UnitLogic unitLogic)
        {
            _rnd = rnd;
            this.unitLogic = unitLogic;
        }

        public FightOutcome CalculateFight(UnitCollection attacker, UnitCollection defender)
        {
            var attackerOfensePower = this.unitLogic.GetMilitaryPower(attacker, MilitarySide.Ofense);
            var attackerDefensePower = this.unitLogic.GetMilitaryPower(attacker, MilitarySide.Defense);
            var defenderOfensePower = this.unitLogic.GetMilitaryPower(defender, MilitarySide.Ofense);
            var defenderDefensePower = this.unitLogic.GetMilitaryPower(defender, MilitarySide.Defense);

            // simplistic approach
            decimal attackersLostPower = attackerDefensePower - defenderOfensePower;
            decimal defendersLostPower = defenderDefensePower - attackerOfensePower;

            uint attackersLostPwr = 0;
            uint defendersLostPwr = 0;

            if (attackersLostPower < 0)
            {
                attackersLostPwr = (uint) this._rnd.NextDecimal(0, Math.Abs(attackersLostPower));
            }
            if (defendersLostPower < 0)
            {
                defendersLostPwr = (uint)this._rnd.NextDecimal(0, Math.Abs(defendersLostPower));
            }

            uint junkAmount = 0;

            var killedAttackers = this.CalculateRazings(attacker, attackersLostPwr, ref junkAmount);
            var killedDefenders = this.CalculateRazings(defender, defendersLostPwr, ref junkAmount);

            string[] defNames = killedDefenders.GetAll().Select(a => $"{a.Unit.Name}:{a.Amount.ToString()}").ToArray();
            Debug.WriteLine($"Defender Loses: [{string.Join(", ", defNames)}]");
            string[] attNames = killedAttackers.GetAll().Select(a => $"{a.Unit.Name}:{a.Amount.ToString()}").ToArray();
            Debug.WriteLine($"Attacker Loses: [{string.Join(", ", attNames)}]");

            return new FightOutcome()
            {
                JunkProduced = new IndexedValue<ResourceInfo, Quantity32>(ResourceInfo.Junk, junkAmount),
                RazedDefenders = killedDefenders,
                RazedAttackers = killedAttackers
            };
        }

        private UnitCollection CalculateRazings(UnitCollection sourceUnit, decimal lostPower, ref uint junkAmount)
        {
            if (lostPower <= 0.0m)
            {
                return UnitCollection.Empty;    
            }

            var amounts = sourceUnit.GetAll().Where(u => u.Amount > 0).ToArray();
            
            // split razings between unit types and randomly deduct
            var powerPerAmount = this.Distribute(lostPower, amounts.Length);
            
            var razedUnits = new UnitCollection();

            for (int i = 0; i < amounts.Length; i++)
            {
                uint razed = Math.Min(amounts[i].Amount, (uint)Math.Round(powerPerAmount[i] / amounts[i].Unit.Defense, 0));
                razedUnits.Add(new UnitAmount(amounts[i].Unit, razed));
            }

            return razedUnits;
        }

        private decimal[] Distribute(decimal lostPower, int length)
        {
            decimal total = lostPower;
            decimal[] dists = new decimal[length];
            for (int i = 0; i < length - 1; ++i)
            {
                decimal dec = this._rnd.NextDecimal(0, total);
                dists[i] = dec;
                total -= dec;
            }
            dists[dists.Length - 1] = total; // remainder

            dists.Shuffle(this._rnd);

            Debug.WriteLine($"{lostPower} has been distributed into [{string.Join(",", dists.Select(d => d.ToString("F0")))}]");
            return dists;
        }
    }

    public class FightOutcome
    {
        public UnitCollection RazedAttackers { get; internal set; }

        public UnitCollection RazedDefenders { get; internal set; }

        public IndexedValue<ResourceInfo, Quantity32> JunkProduced { get; set; }


        public static FightOutcome Empty
        {
            get
            {
                return new FightOutcome()
                {
                    JunkProduced = new IndexedValue<ResourceInfo, Quantity32>(ResourceInfo.Junk, 0),
                    RazedDefenders = UnitCollection.Empty,
                    RazedAttackers = UnitCollection.Empty
                };
            }
        }
    }
}
