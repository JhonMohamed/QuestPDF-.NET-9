namespace Infrastructure;
using Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PdfGenerator : IPdfGenerator
{
    public byte[] GenerateInvoice(string clientName, string amount)
    {
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Content().Column(col =>
                {
                    col.Item().Text($"Invoice for {clientName}").FontSize(20).Bold();
                    col.Item().Text($"Amount: ${amount}").FontSize(16);
                    col.Item().Text($"Date: {DateTime.Now.ToShortDateString()}").FontSize(12);
                });
            });
        });

        return document.GeneratePdf();
    }

 
}