namespace Colony.Model.Resources
{
    using System.Diagnostics;

    using Colony.Model.BaseTypes;
    using Colony.Model.Printers;

    [DebuggerDisplay("ResourceCollection")]
    [Printer(typeof(ResourceCollectionPrinter))]
    public class ResourceCollection : QuantityCollection<ResourceInfo>
    {
        
    }
}