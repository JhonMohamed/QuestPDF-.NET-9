using Infrastructure;
using Application;
using Domain;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Inyectar dependencias
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
builder.Services.AddScoped<GenerateInvoiceCommand>();

// Configuración estándar
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();