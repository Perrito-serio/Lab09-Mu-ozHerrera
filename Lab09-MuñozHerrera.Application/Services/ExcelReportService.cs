 
using ClosedXML.Excel;
using Lab09_MuñozHerrera.Application.Interfaces;
using Lab09_MuñozHerrera.Core.Interfaces;

namespace Lab09_MuñozHerrera.Infrastructure.Services
{
    public class ExcelReportService : IExcelReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcelReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> GenerateSalesByClientReportAsync()
        {
            var salesData = await _unitOfWork.Clients.GetTotalSalesByClientAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Ventas por Cliente");

                var headerRow = worksheet.Row(1);
                headerRow.Cell(1).Value = "Cliente";
                headerRow.Cell(2).Value = "Ventas Totales ($)";
                
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var item in salesData)
                {
                    worksheet.Cell(row, 1).Value = item.ClientName;
                    worksheet.Cell(row, 2).Value = item.TotalSales;
                    worksheet.Cell(row, 2).Style.NumberFormat.Format = "$ #,##0.00"; 
                    row++;
                }

                var range = worksheet.Range(1, 1, row - 1, 2);
                var table = range.CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium9;

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<byte[]> GenerateFullOrdersReportAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllOrdersWithDetailsAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Detalle de Ordenes");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID Orden";
                worksheet.Cell(1, 2).Value = "Fecha";
                worksheet.Cell(1, 3).Value = "Cliente";
                worksheet.Cell(1, 4).Value = "Producto";
                worksheet.Cell(1, 5).Value = "Cantidad";
                worksheet.Cell(1, 6).Value = "Precio Unit.";
                worksheet.Cell(1, 7).Value = "Subtotal";

                var header = worksheet.Range("A1:G1");
                header.Style.Font.Bold = true;
                header.Style.Fill.BackgroundColor = XLColor.DarkGreen;
                header.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var order in orders)
                {
                    foreach (var detail in order.Orderdetails)
                    {
                        worksheet.Cell(row, 1).Value = order.Orderid;
                        worksheet.Cell(row, 2).Value = order.Orderdate;
                        worksheet.Cell(row, 3).Value = order.Client.Name;
                        worksheet.Cell(row, 4).Value = detail.Product.Name;
                        worksheet.Cell(row, 5).Value = detail.Quantity;
                        worksheet.Cell(row, 6).Value = detail.Product.Price;
                        
                        worksheet.Cell(row, 7).FormulaA1 = $"=E{row}*F{row}";
                        
                        row++;
                    }
                }

                worksheet.Column(2).Style.DateFormat.Format = "dd/MM/yyyy";
                worksheet.Column(6).Style.NumberFormat.Format = "$ #,##0.00";
                worksheet.Column(7).Style.NumberFormat.Format = "$ #,##0.00";
                
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}