using Domain;
using Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GenerateInvoiceReport(List<Invoice> invoices)
        {
            // Cargar el logo SVG
            var svgContent = IconHelper.LoadSvgContent("neonnova");

            // Configurar la licencia de QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            // Crear el documento PDF
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Configuración básica de la página
                    page.Margin(40);
                    page.Size(PageSizes.A4);

                    // Fondo blanco para toda la página
                    page.Background().Background("#ffffff"); // Fondo blanco

                    // Encabezado
                    page.Header().Row(row =>
                    {
                        // Columna izquierda: Texto "Boleta de venta"
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Boleta de venta").FontSize(18).FontColor("#6a5bff").SemiBold();
                        });

                        // Columna derecha: Logo
                        row.ConstantItem(150).Column(col =>
                        {
                            // Logo con fondo blanco y tamaño mayor
                            col.Item().AlignCenter()
                                .Width(120) // Ancho mayor
                                .Height(60) // Alto mayor
                                .Svg(svgContent); // Carga el logo SVG
                        });
                    });

                    // Contenido principal
                    page.Content().Column(content =>
                    {
                        // Separador
                        content.Item().PaddingVertical(10).LineHorizontal(1).LineColor("#6a5bff");

                        // Tabla de facturas
                        content.Item().Table(table =>
                        {
                            // Definición de columnas
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(40);  // ID
                                cols.RelativeColumn(2);   // Título
                                cols.RelativeColumn(3);   // Contenido
                                cols.ConstantColumn(80);  // Fecha
                            });

                            // Cabecera de la tabla
                            table.Header(header =>
                            {
                                header.Cell().Background("#6a5bff").Padding(5).Text("ID").FontColor("#ffffff").Bold();
                                header.Cell().Background("#6a5bff").Padding(5).Text("Título").FontColor("#ffffff").Bold();
                                header.Cell().Background("#6a5bff").Padding(5).Text("Contenido").FontColor("#ffffff").Bold();
                                header.Cell().Background("#6a5bff").Padding(5).Text("Fecha").FontColor("#ffffff").Bold();
                            });

                            // Filas de datos
                            foreach (var inv in invoices)
                            {
                                table.Cell().Text(inv.Id.ToString()).AlignCenter();
                                table.Cell().Text(inv.Title).AlignLeft();
                                table.Cell().Text(inv.Content).AlignLeft();
                                table.Cell().Text(inv.CreatedAt.ToString("dd/MM/yyyy")).AlignCenter();
                            }
                        });
                    });

                    // Pie de página
                    page.Footer().Row(row =>
                    {
                        // Información de contacto en la parte izquierda
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingBottom(5).Text("RUC: 20602345678").FontSize(10).FontColor("#6a5bff");
                            col.Item().PaddingBottom(5).Text("Dirección: Av. Larco 782, Lima").FontSize(10);
                            col.Item().PaddingBottom(5).Text("Teléfono: +51 951 890 315 | jhon3122@gmail.com").FontSize(10);
                        });

                        // Fecha de emisión en la parte derecha
                        row.ConstantItem(150).Column(col =>
                        {
                            col.Item().PaddingTop(10).Text("Fecha de emisión:")
                                .FontSize(10)
                                .Bold()
                                .FontColor("#6a5bff");

                            col.Item().Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                .FontSize(12)
                                .Bold();
                        });
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}