namespace Colony.Model.Printers
{
    using System;

    /// <summary>
    /// Specifies Debug Printer class that prints state of the object
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PrinterAttribute : Attribute
    {
        public PrinterAttribute(Type printerType)
        {
            this.PrinterType = printerType;
        }

        public Type PrinterType { get; private set; }
    }
}