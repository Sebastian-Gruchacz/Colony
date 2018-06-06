namespace Colony.Model.Printers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public abstract class BasePrinter<T> : IStatePrinter<T>
    {
        protected IEnumerable<string> GetPrintedPart(object obj)
        {
            if (obj == null)
            {
                yield return "[NULL]";
                yield break;
            }

            List<string> lines = new List<string>();
            using (var memStr = new MemoryStream())
            {
                using (var wr = new StreamWriter(memStr, Encoding.Unicode))
                {
                    var printer = StatePrinters.For(obj);
                    printer.PrintState(obj, wr);
                    wr.Flush();
                    
                    memStr.Position = 0;
                    using (var r = new StreamReader(memStr, Encoding.Unicode))
                    {
                        string line = null;
                        while ((line = r.ReadLine()) != null)
                        {
                            yield return line;
                        }
                    }
                }
            }
        }

        protected void WriteIntendedMultiline(TextWriter outStream, uint tabCount, params string[] lines)
        {
            string indent = new string('\t', (int)tabCount);
            foreach (string line in lines)
            {
                outStream.WriteLine($"{indent}{line}");
            }
        }

        public abstract void PrintState(T obj, TextWriter outStream);

        public void PrintState(object obj, TextWriter outStream)
        {
            this.PrintState((T)obj, outStream);
        }
    }
}