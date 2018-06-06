using System.Linq;

namespace Colony.Model.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Colony.Model.Printers;
    using Colony.Model.Units;

    [Printer(typeof(UnitCollectionPrinter))]
    public class UnitCollection// : IEnumerable<UnitAmount>
    {
        //public IEnumerator<UnitAmount> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        //public uint this[UnitInfo index]
        //{
        //    get {  /* return the specified index here */ }
        //    set
        //    {
        //        /* set the specified index to value here */
        //    }
        //}

        readonly Dictionary<UnitInfo, uint> _units = new Dictionary<UnitInfo, uint>();

        public UnitCollection(params UnitAmount[] unitAmounts)
        {
            foreach (var unitAmount in unitAmounts)
            {
                // TODO: protect uniquenes
                _units.Add(unitAmount.Unit, unitAmount.Amount);
            }
        }


        public bool AnyOne()
        {
            var u = this._units.FirstOrDefault(vk => vk.Value > 0).Key;
            return u != null;
        }

        public void Substract(UnitCollection manySubstract)
        {
            foreach (var razedDefender in manySubstract._units)
            {
                if (this._units.ContainsKey(razedDefender.Key))
                {
                    uint currentAmount = this._units[razedDefender.Key];
                    if (currentAmount > razedDefender.Value)
                    {
                        currentAmount -= razedDefender.Value;
                    }
                    else
                    {
                        currentAmount = 0;
                    }
                    this._units[razedDefender.Key] = currentAmount;
                }
                else
                {
                    this._units.Add(razedDefender.Key, 0);
                }
            }
        }



        public static UnitCollection Empty
        {
            get
            {
                return new UnitCollection();
            }
        }

        public IEnumerable<UnitAmount> GetAll()
        {
            foreach (KeyValuePair<UnitInfo, uint> kvp in this._units)
            {
                yield return new UnitAmount(kvp.Key, kvp.Value);
            }
        }

        public UnitCollection Clone()
        {
            return new UnitCollection(this.GetAll().ToArray());
        }

        public void Add(UnitAmount unitAmount)
        {
            if (this._units.ContainsKey(unitAmount.Unit))
            {
                uint currentAmount = this._units[unitAmount.Unit];
                this._units[unitAmount.Unit] = currentAmount + unitAmount.Amount;
            }
            else
            {
                this._units.Add(unitAmount.Unit, unitAmount.Amount);
            }
        }
    }
}