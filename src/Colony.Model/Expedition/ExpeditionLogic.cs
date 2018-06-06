namespace Colony.Model.Expedition
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Colony.Model.BaseTypes;
    using Colony.Model.Core;
    using Colony.Model.Fight;
    using Colony.Model.Resources;

    /// <summary>
    /// Class that calculates logic regarding expedition
    /// </summary>
    public class ExpeditionLogic
    {
        private const decimal MILITARY_RANGE_FACTOR = 0.30m;
        private const decimal LABOUR_RANGE_FACTOR = 0.80m;
        private const decimal BASE_RANGE_DELTA = 100;
        private const decimal MIN_RANGE_DELTA = 50;
        private const decimal RANGE_RESOURCE_FINDING_PROBABILITY_FACTOR = 0.3m;
        private const decimal RESOURCE_FINDING_POWERS_FACTOR = 0.2m;


        private readonly RandomProvider _rnd;
        private readonly UnitLogic _unitLogic;
        private readonly FightLogic _fightLogic;
        

        public ExpeditionLogic(RandomProvider rnd, UnitLogic unitLogic, FightLogic fightLogic)
        {
            if (rnd == null) throw new ArgumentNullException(nameof(rnd));
            if (unitLogic == null) throw new ArgumentNullException(nameof(unitLogic));

            _rnd = rnd;
            this._unitLogic = unitLogic;
            this._fightLogic = fightLogic;
        }

        public void ProcessExpedition(ExpeditionData data, uint currentRoundNumber)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (currentRoundNumber < data.StartingRound || currentRoundNumber > data.EndingRound)
            {
                return; // already processed
            }

            decimal militaryPower = this.CalculateCurrentExpeditionPower(data, PowerType.Military); // increase defense and range
            decimal sciencePower = this.CalculateCurrentExpeditionPower(data, PowerType.Science); // increases rare findings per turn
            decimal laborPower = this.CalculateCurrentExpeditionPower(data, PowerType.Labour); // increase range & findings per turn

            decimal rangeDelta = this.CalculateRangeIncrease(militaryPower, laborPower);
            data.CurrentRange += rangeDelta;

            // calculate new findings - depending on passing turn and total range
            var explorableResources = ResourceInfo.KnownResources.Where(r =>
                r.FindingProbability > 0.0m && r.BaseFindSize > 0.0m);
            foreach (var explorableResource in explorableResources)
            {
                var finding = this.CalculateFindingSize(explorableResource, militaryPower, sciencePower,
                    laborPower, data.CurrentRange);
                data.DiscoveredResources.Add(finding);
            }

            // calculate endangerment (if any) depending on passing turn (& range)
            decimal dangerLevel = this.CalculateDangerLevel(data.CurrentRange, data.StartingRound, currentRoundNumber);

            // fight
            var enemyForces = this.GenerateOpposingForce(dangerLevel);
            if (enemyForces.AnyOne())
            {
                FightOutcome outcome = this._fightLogic.CalculateFight(enemyForces, data.RemainingUnits);
                if (outcome.JunkProduced.Value > 0)
                {
                    data.DiscoveredResources.Add(outcome.JunkProduced);
                }

                // reduce units
                if (outcome.RazedDefenders.AnyOne())
                {
                    data.RemainingUnits.Substract(outcome.RazedDefenders);
                }
            }

            // left at least one worker and stop expedition at that point
            if (!data.RemainingUnits.AnyOne())
            {
                this._unitLogic.AddWorkers(data.RemainingUnits, 1);
                data.EndingRound = currentRoundNumber;
            }
        }

        private IndexedValue<ResourceInfo, Quantity32> CalculateFindingSize(ResourceInfo resourceInfo, 
            decimal militaryPower, decimal sciencePower, decimal laborPower,
            decimal currentRange)
        {
            if (this._rnd.NextDecimal() <
                resourceInfo.FindingProbability*currentRange*RANGE_RESOURCE_FINDING_PROBABILITY_FACTOR)
            {
                // base amount
                decimal amount = this._rnd.NextDecimal()*resourceInfo.BaseFindSize;
                
                // total powers impact
                amount += this._rnd.NextDecimal(amount, amount + (militaryPower + sciencePower + laborPower)*resourceInfo.BaseFindSize*
                                                RESOURCE_FINDING_POWERS_FACTOR / 3.0m);
                // specific powers impact
                switch (resourceInfo.ResourceType)
                {
                    case Resource.Silicon:
                    {
                        amount += this._rnd.NextDecimal(amount,
                            amount + laborPower*resourceInfo.BaseFindSize*RESOURCE_FINDING_POWERS_FACTOR);
                        break;
                    }
                    case Resource.IronOre:
                    {
                        amount += this._rnd.NextDecimal(amount,
                            amount + sciencePower*resourceInfo.BaseFindSize*RESOURCE_FINDING_POWERS_FACTOR);
                        break;
                    }
                    case Resource.UraniumOre:
                    {
                        amount += this._rnd.NextDecimal(amount,
                            amount + militaryPower*resourceInfo.BaseFindSize*RESOURCE_FINDING_POWERS_FACTOR);
                        break;
                    }
                    case Resource.Food:
                        break;
                    case Resource.Junk:
                    {
                        amount += this._rnd.NextDecimal(amount,
                            amount + (laborPower + sciencePower)*resourceInfo.BaseFindSize*RESOURCE_FINDING_POWERS_FACTOR) / 2;
                        break;
                    }
                }

                return new IndexedValue<ResourceInfo, Quantity32>(resourceInfo, (uint)amount);
            }
            else
            {
                return new IndexedValue<ResourceInfo, Quantity32>(resourceInfo, 0);
            }
        }

        private UnitCollection GenerateOpposingForce(decimal dangerLevel)
        {
            if (this._rnd.NextDecimal() < dangerLevel) // TODO: improve
            {
                return this._unitLogic.GenerateRandomForces(dangerLevel);
            }
            else
            {
                return UnitCollection.Empty;
            }
        }

        private decimal CalculateDangerLevel(decimal currentRange, uint startingRound, uint roundNumber)
        {
            // http://stackoverflow.com/questions/3745760/java-generating-a-random-numbers-with-a-logarithmic-distribution
            uint roundsPassed = roundNumber - startingRound + 1; // one - based
            decimal danger = (decimal)Math.Floor(Math.Log((double)(this._rnd.NextDecimal(0.0m, currentRange) * roundsPassed) + 1.0) / Math.Log(2));
            danger = danger / 50.0m;

            Debug.WriteLine($"Calculated DangerLevel {danger} for range {currentRange} and expedition round {roundsPassed}");

            return danger;
        }

        private decimal CalculateRangeIncrease(decimal militaryPower, decimal laborPower)
        {
            // TODO: finish equation - this is pure quessing
            // probably would like to achieve logarythmic scale...
            return this._rnd.NextDecimal(MIN_RANGE_DELTA, (BASE_RANGE_DELTA*militaryPower*MILITARY_RANGE_FACTOR) + (BASE_RANGE_DELTA*laborPower*LABOUR_RANGE_FACTOR) + MIN_RANGE_DELTA);
        }

        private decimal CalculateCurrentExpeditionPower(ExpeditionData data, PowerType powerType)
        {
            switch (powerType)
            {
                case PowerType.Military:
                    return this._unitLogic.GetMilitaryPower(data.RemainingUnits, MilitarySide.Both);
                case PowerType.Science:
                    return this._unitLogic.GetSciencePower(data.RemainingUnits);
                case PowerType.Labour:
                    return this._unitLogic.GetLabourPower(data.RemainingUnits);
                case PowerType.Electric:
                    throw new NotSupportedException("Power.Electric does not apply to Expedition calculations.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerType), powerType, null);
            }
        }
    }
}