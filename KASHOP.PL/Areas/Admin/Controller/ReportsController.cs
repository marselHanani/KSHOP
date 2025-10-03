using KASHOP.BLL.Service.classes;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controller;

[ApiController]
[Route("api/[area]/[controller]")]
[Area("Admin")]
public class ReportsController(ReportService reportService) : ControllerBase
{
    [HttpGet("productPdf")]
    public async Task<IActionResult> DownloadProductReport()
    {
        try
        {
            var pdfBytes = await reportService.GenerateProductReport();

            return File(pdfBytes, "application/pdf", "product-report.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error generating report: {ex.Message}");
        }
    }

}