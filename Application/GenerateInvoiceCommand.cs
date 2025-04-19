
using Domain;
using Domain.Interfaces;
using Infrastructure;

namespace Application;


public interface IGenerateInvoiceCommand
{
    Task<byte[]> ExecuteAsync();
}

public class GenerateInvoiceCommand : IGenerateInvoiceCommand
{
    private readonly IInvoiceService _service;
    private readonly IPdfGenerator _pdfGen;

    public GenerateInvoiceCommand(
        IInvoiceService service,
        IPdfGenerator pdfGen)
    {
        _service = service;
        _pdfGen = pdfGen;
    }

    public async Task<byte[]> ExecuteAsync()
    {
        List<Domain.Invoice> invoices = await _service.ObtenerFacturasAsync();
        return _pdfGen.GenerateInvoiceReport(invoices);
    }
}
