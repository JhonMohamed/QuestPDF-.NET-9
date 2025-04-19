using Domain;
using Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;

namespace Infrastructure
{
    public class PdfGenerator : IPdfGenerator 
    {
        public byte[] GenerateInvoice(string customerName, string product)
        {
            // 1. Obtén el SVG como contenido
            var svgContent = IconHelper.LoadSvgContent("game");

            // 2. Configura licencia (Community por defecto)
            QuestPDF.Settings.License = LicenseType.Community;

            // 3. Crea el documento
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header con SVG insertado
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Neon Nova")
                                .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                            col.Item().Text("Recibo de Compra")
                                .FontSize(14).FontColor(Colors.Grey.Medium);
                        });

                        row.ConstantItem(60)
                           .Height(60)
                           .Svg(svgContent)
                          ;  // ¡SVG directo como vector!
                    });

                    // Cuerpo del recibo
                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Row(r =>
                        {
                            r.RelativeItem().Text($"Cliente: {customerName}");
                            r.RelativeItem().AlignRight().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}");
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.ConstantColumn(80);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Text("Descripción").SemiBold();
                                h.Cell().AlignRight().Text("Total").SemiBold();
                            });

                            table.Cell().Text(product);
                            table.Cell().AlignRight().Text("$99.99");
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("¡Gracias por su compra!")
                         .FontSize(10)
                         .FontColor(Colors.Grey.Medium)
                         .Italic();
                    });
                });
            });

            // 4. Genera el PDF en memoria
            return document.GeneratePdf();
        }
    }
}
