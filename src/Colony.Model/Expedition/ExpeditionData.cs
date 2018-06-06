namespace Colony.Model.Expedition
{
    using Colony.Model.Core;
    using Colony.Model.Printers;
    using Colony.Model.Resources;

    [Printer(typeof(ExpeditionDataPrinter))]
    public class ExpeditionData
    {
        public Player Owner { get; set; }

        public uint StartingRound { get; set; }

        public uint EndingRound { get; set; }

        /// <summary>
        /// Starting unit condition
        /// </summary>
        public UnitCollection AssignedUnits { get; set; }

        /// <summary>
        /// Units remaining after previous calculations
        /// </summary>
        public UnitCollection RemainingUnits { get; set; }

        /// <summary>
        /// Number of resources discovered so far
        /// </summary>
        public ResourceCollection DiscoveredResources { get; set; }

        /// <summary>
        /// Expedition range achieved so far
        /// </summary>
        public decimal CurrentRange { get; set; }

    }
}
