namespace Colony.Model.Printers
{
    using System.IO;

    public interface IStatePrinter<T> : IStatePrinter
    {
        void PrintState(T obj, TextWriter outStream);
    }

    public interface IStatePrinter
    {
        void PrintState(object obj, TextWriter outStream);
    }
}