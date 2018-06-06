namespace Colony.Model.Printers
{
    using System.IO;
    using System.Linq;

    using Colony.Model.Expedition;

    public class ExpeditionDataPrinter : BasePrinter<ExpeditionData>
    {
        private const string SEPARATOR = @"====================================================";

        public override void PrintState(ExpeditionData obj, TextWriter outStream)
        {
            outStream.WriteLine(SEPARATOR);
            outStream.WriteLine($"State of [{obj.GetType().FullName}]");
            outStream.WriteLine($"Player:        {obj.Owner?.Name ?? "[NULL]"}");
            outStream.WriteLine($"CurrentRange:  {obj.CurrentRange}");
            outStream.WriteLine($"StartingRound: {obj.StartingRound}");
            outStream.WriteLine($"EndingRound:   {obj.EndingRound}");
            outStream.WriteLine($"AssignedUnits:");
            base.WriteIntendedMultiline(outStream, 1, base.GetPrintedPart(obj.AssignedUnits).ToArray());
            outStream.WriteLine($"RemainingUnits:");
            base.WriteIntendedMultiline(outStream, 1, base.GetPrintedPart(obj.RemainingUnits).ToArray());
            outStream.WriteLine($"DiscoveredResources:");
            base.WriteIntendedMultiline(outStream, 1, base.GetPrintedPart(obj.DiscoveredResources).ToArray());
            outStream.WriteLine(SEPARATOR);
        }

        
    }
}
