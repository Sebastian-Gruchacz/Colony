namespace Colony.Model.Printers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class StatePrinters
    {
        private static Dictionary<Type, object> printers = new Dictionary<Type, object>();

        public static IStatePrinter<T> For<T>(T record)
        {
            object printer;
            Type t = record.GetType();
            if (!printers.TryGetValue(t, out printer))
            {
                printer = CreateNewPrinter(t);
                printers.Add(t, printer);
            }
            return (IStatePrinter<T>)printer;
        }

        public static IStatePrinter For(object record)
        {
            object printer;
            Type t = record.GetType();
            if (!printers.TryGetValue(t, out printer))
            {
                printer = CreateNewPrinter(t);
                printers.Add(t, printer);
            }
            return (IStatePrinter)printer;
        }

        private static object CreateNewPrinter(Type recordType)
        {
            var attrib = recordType.GetCustomAttributes(typeof(PrinterAttribute), inherit: true).SingleOrDefault();
            if (attrib != null)
            {
                Type printerType = ((PrinterAttribute)attrib).PrinterType;
                var printMethod = printerType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(m => m.Name.Equals(nameof(IStatePrinter<object>.PrintState)));

                var parameters = printMethod.GetParameters();

                if (!(parameters[0].ParameterType == recordType))
                {
                    throw new NotImplementedException($"[{printerType.FullName}] is not valid printer for [{recordType.FullName}] type.");
                }

                object printer = Activator.CreateInstance(printerType);
                return printer;
            }
            else
            {
                throw new NotImplementedException($"Printer class not specified for type [{recordType.FullName}]");
            }
        }
    }
}