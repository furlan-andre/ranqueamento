using Microsoft.EntityFrameworkCore;
using Ranqueamento.API.Configuracao;
using Ranqueamento.API.DataBase;
using Ranqueamento.API.Helpers;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Models;
using Ranqueamento.API.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
builder.Services.AddDbContext<ProjetoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Ranqueamento")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IConfiguracoes, Configuracoes>();
    services.AddScoped<IVerificaPessoaMenoridade, VerificaPessoaMenoridade>();    
    services.AddScoped<IObterDependentesPontuantes, ObterDependentesPontuantes>();
    services.AddScoped<ICalcularPontosRenda, CalcularPontosRenda>();
    services.AddScoped<ICalcularPontosDependente, CalcularPontosDependente>();
    services.AddScoped<ICalcularPontos, CalcularPontos>();
    services.AddScoped<IRanqueadorFamilias, RanqueadorFamilias>();
    services.AddScoped<IFamiliaRepositorio, FamiliaRepositorio>();
    
}