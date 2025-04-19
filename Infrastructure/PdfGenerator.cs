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
        public byte[] GenerateInvoiceReport(List<Invoice> invoices)
        {
            // Carga SVG directamente de archivo (asegúrate de copiar a output)
            var svgContent = IconHelper.LoadSvgContent("game");

            QuestPDF.Settings.License = LicenseType.Community;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header con título y SVG
                    page.Header().Row(r =>
                    {
                        r.RelativeItem().Text("Reporte de Facturas")
                            .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                        r.ConstantItem(60).Height(60).Svg(svgContent);
                    });

                    // Tabla de facturas
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(40);
                            cols.RelativeColumn();
                            cols.RelativeColumn();
                            cols.ConstantColumn(80);
                        });

                        // Cabecera
                        table.Header(h =>
                        {
                            h.Cell().Text("ID").SemiBold();
                            h.Cell().Text("Título").SemiBold();
                            h.Cell().Text("Contenido").SemiBold();
                            h.Cell().AlignRight().Text("Fecha").SemiBold();
                        });

                        // Filas
                        foreach (var inv in invoices)
                        {
                            table.Cell().Text(inv.Id.ToString());
                            table.Cell().Text(inv.Title);
                            table.Cell().Text(inv.Content);
                            table.Cell().AlignRight()
                                 .Text(inv.CreatedAt.ToString("dd/MM/yyyy"));
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("Generado el ");
                        t.Span(DateTime.Now.ToString("dd/MM/yyyy")).SemiBold();
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}