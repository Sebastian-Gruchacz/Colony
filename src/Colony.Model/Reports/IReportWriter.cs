namespace Colony.Model.Reports
{
    using System.IO;

    public interface IReportWriter
    {
        void WriteReportInto(TextWriter stream);
    }
}