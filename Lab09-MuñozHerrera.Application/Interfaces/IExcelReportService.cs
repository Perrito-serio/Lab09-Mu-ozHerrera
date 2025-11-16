namespace Lab09_MuñozHerrera.Application.Interfaces
{
    public interface IExcelReportService
    {
        Task<byte[]> GenerateSalesByClientReportAsync();

        Task<byte[]> GenerateFullOrdersReportAsync();
    }
}