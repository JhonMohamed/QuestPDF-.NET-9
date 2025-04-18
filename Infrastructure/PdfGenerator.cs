using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace Infrastructure
{
    public interface IPdfGenerator
    {
        byte[] GenerateInvoice(string customerName, string product);
    }

    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GenerateInvoice(string customerName, string product)
        {
            // 1) Carga el SVG desde disco como SvgImage
            var svgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infrastructure", "Icons", "game.svg");
            if (!File.Exists(svgPath))
                throw new FileNotFoundException($"No se encontró el SVG en: {svgPath}");

            var svgImage = SvgImage.FromFile(svgPath);

            // 2) Crea el documento
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // — Header —
                    page.Header().Row(row =>
                    {
                        // Izquierda: nombre de la empresa
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Neon Nova")
                                .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                            col.Item().Text("Recibo de Compra")
                                .FontSize(14).FontColor(Colors.Grey.Medium);
                        });

                        // Derecha: icono SVG
                        row.ConstantItem(60)
                           .Height(60)
                           .Element(x => x.Svg(svgImage));
                    });

                    // — Content —
                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Spacing(10);

                        // Cliente y Fecha
                        col.Item().Row(r =>
                        {
                            r.RelativeItem().Text($"Cliente: {customerName}");
                            r.RelativeItem().AlignRight().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}");
                        });

                        // Separador
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Tabla de producto
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Descripción").SemiBold();
                                header.Cell().AlignRight().Text("Total").SemiBold();
                            });

                            // Fila de ejemplo
                            table.Cell().Text(product);
                            table.Cell().AlignRight().Text("$99.99");
                        });
                    });

                    // — Footer —
                    page.Footer().AlignCenter().Element(x =>
                    {
                        x.Text(t =>
                        {
                            t.Span("¡Gracias por su compra!")
                             .FontSize(10)
                             .FontColor(Colors.Grey.Medium)
                             .Italic();
                        });
                    });
                });
            });

            // 3) Genera y retorna el PDF en memoria
            using var ms = new MemoryStream();
            document.GeneratePdf(ms);
            return ms.ToArray();
        }
    }
}
