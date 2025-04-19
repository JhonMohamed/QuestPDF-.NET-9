?? ReportQuestPDF
??? Application
?   ??? Application.csproj
?   ??? GenerateInvoiceCommand.cs
?
??? Domain
?   ??? Domain.csproj
?   ??? Invoice.cs
?   ??? Interfaces
?       ??? IPdfGenerator.cs
?
??? Infrastructure
?   ??? Infrastructure.csproj
?   ??? Data
?   ?   ??? ApplicationDbContext.cs
?   ??? Icons
?   ?   ??? game.svg
?   ??? Migrations
?   ?   ??? 20250418225528_InitialCreate.cs
?   ?   ??? ApplicationDbContextModelSnapshot.cs
?   ??? IconHelper.cs
?   ??? InvoiceService.cs
?   ??? PdfGenerator.cs
?
??? Presentation
?   ??? Presentation.csproj
?   ??? Controllers
?   ?   ??? ReportController.cs
?   ??? Properties
?   ?   ??? launchSettings.json
?   ??? appsettings.json
?   ??? appsettings.Development.json
?   ??? Program.cs
?   ??? Startup.cs (si lo tienes)
?
??? .gitignore
??? .gitattributes
## ?? Funcionalidad

- Generación de reportes en formato PDF con **QuestPDF**.
- Renderizado de íconos SVG usando **SkiaSharp.Svg**.
- Estructura desacoplada por capas (Clean Architecture).
- API REST construida con ASP.NET Core 9.
- Documentación automática con Swagger.

---

## ?? ¿Cómo funciona?

1. El usuario realiza una solicitud POST al endpoint `/api/report`.
2. La capa `Application` maneja el comando de generación de factura.
3. `Infrastructure` implementa la lógica para construir el PDF:
   - Lee un ícono SVG incrustado (`game.svg`) con `IconHelper`.
   - Usa `QuestPDF` para crear un layout profesional con encabezado, cuerpo y pie de página.
4. El PDF se genera en memoria y se devuelve como un `byte[]` para ser descargado.

---

## ?? proyecto-paquetes

### ? Application

**Propósito**: Lógica de aplicación (coordinación de casos de uso).

**Paquetes usados**:
- *Ningún paquete externo requerido.*

---

### ? Domain

**Propósito**: Contiene interfaces y modelos del dominio.

**Paquetes usados**:
- *Ningún paquete externo requerido.*

---

### ? Infrastructure

**Propósito**: Implementa lógica de negocio y servicios externos como generación de PDFs y acceso a base de datos.

**Paquetes NuGet utilizados**:

- `Microsoft.EntityFrameworkCore (9.0.4)`
- `Microsoft.EntityFrameworkCore.SqlServer (9.0.4)`
- `Microsoft.EntityFrameworkCore.Tools (9.0.4)`
- `QuestPDF (2025.4.0)`
- `SkiaSharp (3.116.0)`
- `SkiaSharp.NativeAssets.Win32 (3.116.1)`
- `SkiaSharp.Svg (1.60.0)`

---

### ? Presentation

**Propósito**: API Web construida en ASP.NET Core 9.

**Paquetes NuGet utilizados**:

- `Microsoft.AspNetCore.OpenApi (9.0.4)`
- `DotSwashbuckle.AspNetCore (3.0.11)`
- `Microsoft.EntityFrameworkCore (9.0.4)`
- `Microsoft.EntityFrameworkCore.Tools (9.0.4)`

---

## ?? Extras

- ?? Compatible con .NET 9.
- ?? Separa claramente responsabilidades por capa.
- ?? Basado en buenas prácticas de Clean Architecture.
- ?? Ideal para extensiones como reportes dinámicos, logs o base de datos persistente.

---

## ?? Autor

Jhon Wilson Rodriguez Quezada  
Correo: jhon3122ahre@gmail.com  
Instituciones: Cibertec | CIDUNT  