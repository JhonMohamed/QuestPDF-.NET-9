using Infrastructure;
using Infrastructure.Data;
using Application;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("cnx");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString, sqlOptions =>
//        sqlOptions.MigrationsAssembly("Infrastructure")));


var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde la configuración
var connectionString = builder.Configuration.GetConnectionString("cnx");

// Configurar ApplicationDbContext con el ensamblaje de migraciones
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Infrastructure")));

// Inyectar dependencias
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
builder.Services.AddScoped<GenerateInvoiceCommand>();

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