namespace Colony.Model.Printers
{
    using System.IO;

    using Colony.Model.Core;

    public class ResourceCollectionPrinter : BasePrinter<ResourceCollection>
    {
        public override void PrintState(ResourceCollection obj, TextWriter outStream)
        {
            foreach (ResourceAmount amount in obj.GetAll())
            {
                outStream.WriteLine($"{amount.Resource.Name}:    {amount.Amount}");
            }
        }
    }
}