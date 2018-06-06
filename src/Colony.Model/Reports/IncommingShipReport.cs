using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony.Model.Reports
{
    using System.IO;

    /// <summary>
    /// Used to present report about ship that landed in this turn
    /// </summary>
    public class IncommingShipReport : IReportWriter
    {
        public IncommingShipReport(string shipName, string shipClass, uint shipSize)
        {
            this.ShipName = shipName;
            this.ShipClass = shipClass;
            this.ShipSize = shipSize;
        }

        public string ShipName { get; private set; }

        public string ShipClass { get; private set; }

        public uint ShipSize { get; private set; }

        /// <summary>
        /// Specifies owner of the Ship - that determines prices, content, rarity etc... Corporations are shared between players as wel as Alien races and NPCs
        /// </summary>
        public Player ShipCorporation { get; set; }

        public void WriteReportInto(TextWriter stream)
        {
            throw new NotImplementedException();
        }
    }
}
