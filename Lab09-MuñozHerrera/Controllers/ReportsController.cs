using Lab09_MuñozHerrera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab09_MuñozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IExcelReportService _reportService;

        public ReportsController(IExcelReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("sales-by-client")]
        public async Task<IActionResult> GetSalesByClientReport()
        {
            try
            {
                var fileContent = await _reportService.GenerateSalesByClientReportAsync();
                string fileName = $"Ventas_Clientes_{DateTime.Now:yyyyMMdd}.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                
                return File(fileContent, contentType, fileName);
            }
            catch (Exception ex)
            {
                return Problem($"Error generando el reporte: {ex.Message}");
            }
        }

        [HttpGet("orders-detail")]
        public async Task<IActionResult> GetOrdersDetailReport()
        {
            try
            {
                var fileContent = await _reportService.GenerateFullOrdersReportAsync();
                string fileName = $"Detalle_Ordenes_{DateTime.Now:yyyyMMdd}.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(fileContent, contentType, fileName);
            }
            catch (Exception ex)
            {
                return Problem($"Error generando el reporte: {ex.Message}");
            }
        }
    }
}