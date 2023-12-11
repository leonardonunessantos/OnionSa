using Microsoft.EntityFrameworkCore;
using OnionSa.Application.Services.Helpers;
using OnionSa.Application.Services.Implementations;
using OnionSa.Application.Services.Interfaces;
using OnionSa.Infrastructure.Persistence;
using OnionSa.Infrastructure.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("OnionSaCs");
builder.Services.AddDbContext<OnionSaDbContext>(options => options.UseInMemoryDatabase("OnionSa"));
using var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<OnionSaDbContext>();
SeedData.Initialize(dbContext);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISpreadsheetService, SpreadsheetService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IViaCepService, ViaCepService>();
builder.Services.AddScoped<RegionMapper>();
builder.Services.AddScoped<IOrderService, OrderService>();


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
