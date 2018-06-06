namespace Colony.Model.Printers
{
    using System.IO;

    using Colony.Model.Core;

    public class UnitCollectionPrinter : BasePrinter<UnitCollection>
    {
        public override void PrintState(UnitCollection obj, TextWriter outStream)
        {
            foreach (UnitAmount amount in obj.GetAll())
            {
                outStream.WriteLine($"{amount.Unit.Name}s:    {amount.Amount}");
            }
        }
    }
}