using Infrastructure;
using Infrastructure.Data;
using Application;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("cnx");

// Configurar ApplicationDbContext con el ensamblaje de migraciones
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Infrastructure")));

// Registrar servicios e interfaces necesarias
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
builder.Services.AddScoped<IGenerateInvoiceCommand, GenerateInvoiceCommand>();

// Configuración estándar
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();