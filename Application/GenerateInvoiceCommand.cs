
using Domain;
using Domain.Interfaces;

namespace Application;


public class GenerateInvoiceCommand
{
    private readonly IPdfGenerator _pdfGenerator;

    public GenerateInvoiceCommand(IPdfGenerator pdfGenerator)
    {
        _pdfGenerator = pdfGenerator;
    }

    public byte[] Execute(string title, string content)
    { 
        return _pdfGenerator.GenerateInvoice(title, content);
    }
}
