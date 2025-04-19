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

- Generaci�n de reportes en formato PDF con **QuestPDF**.
- Renderizado de �conos SVG usando **SkiaSharp.Svg**.
- Estructura desacoplada por capas (Clean Architecture).
- API REST construida con ASP.NET Core 9.
- Documentaci�n autom�tica con Swagger.

---

## ?? �C�mo funciona?

1. El usuario realiza una solicitud POST al endpoint `/api/report`.
2. La capa `Application` maneja el comando de generaci�n de factura.
3. `Infrastructure` implementa la l�gica para construir el PDF:
   - Lee un �cono SVG incrustado (`game.svg`) con `IconHelper`.
   - Usa `QuestPDF` para crear un layout profesional con encabezado, cuerpo y pie de p�gina.
4. El PDF se genera en memoria y se devuelve como un `byte[]` para ser descargado.

---

## ?? proyecto-paquetes

### ? Application

**Prop�sito**: L�gica de aplicaci�n (coordinaci�n de casos de uso).

**Paquetes usados**:
- *Ning�n paquete externo requerido.*

---

### ? Domain

**Prop�sito**: Contiene interfaces y modelos del dominio.

**Paquetes usados**:
- *Ning�n paquete externo requerido.*

---

### ? Infrastructure

**Prop�sito**: Implementa l�gica de negocio y servicios externos como generaci�n de PDFs y acceso a base de datos.

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

**Prop�sito**: API Web construida en ASP.NET Core 9.

**Paquetes NuGet utilizados**:

- `Microsoft.AspNetCore.OpenApi (9.0.4)`
- `DotSwashbuckle.AspNetCore (3.0.11)`
- `Microsoft.EntityFrameworkCore (9.0.4)`
- `Microsoft.EntityFrameworkCore.Tools (9.0.4)`

---

## ?? Extras

- ?? Compatible con .NET 9.
- ?? Separa claramente responsabilidades por capa.
- ?? Basado en buenas pr�cticas de Clean Architecture.
- ?? Ideal para extensiones como reportes din�micos, logs o base de datos persistente.

---

## ?? Autor

Jhon Wilson Rodriguez Quezada  
Correo: jhon3122ahre@gmail.com  
Instituciones: Cibertec | CIDUNT  