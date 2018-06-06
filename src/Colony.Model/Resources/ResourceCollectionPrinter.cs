namespace Colony.Model.Resources
{
    using System.IO;

    using Colony.Model.Printers;

    public class ResourceCollectionPrinter : BasePrinter<ResourceCollection>
    {
        public override void PrintState(ResourceCollection obj, TextWriter outStream)
        {
            foreach (var amount in obj.GetAll())
            {
                outStream.WriteLine($"{amount.Index.Name}:    {amount.Value}");
            }
        }
    }
}