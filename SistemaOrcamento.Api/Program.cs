using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Api.Handlers;
using SistemaOrcamento.Core.Handlers;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddTransient<IClienteHandler, ClienteHandler>();
builder.Services.AddTransient<IVeiculoHandler, VeiculoHandler>();
builder.Services.AddTransient<IProdutoHandler, ProdutoHandler>();
builder.Services.AddTransient<IOrcamentoHandler, OrcamentoHandler>();

builder.Services.AddScoped<ClienteHandler>();

builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => 
    x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();