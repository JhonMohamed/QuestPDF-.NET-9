
namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly GenerateInvoiceCommand _generateInvoiceCommand;

    public ReportController(GenerateInvoiceCommand generateInvoiceCommand)
    {
        _generateInvoiceCommand = generateInvoiceCommand;
    }

    [HttpGet("generate")]
    public IActionResult Generate()
    {
        var pdfBytes = _generateInvoiceCommand.Execute("Factura", "Este es un ejemplo de PDF con QuestPDF");
        return File(pdfBytes, "application/pdf", "Factura.pdf");
    }

}
