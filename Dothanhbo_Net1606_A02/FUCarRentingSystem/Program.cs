using DataAcessObjects.DAO;
using FUCarRentingSystem.Mapper;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Repositories.Repositories;
using Repositories.Repositories.Imple;
using BusinessObjects.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<CarDAO>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<CustomerDAO>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CarProducerDAO>();
builder.Services.AddScoped<ICarProducerRepository, CarProducerRepository>();
builder.Services.AddScoped<CarRentalDAO>();
builder.Services.AddScoped<ICarRentalRepository, CarRentalRepository>();
builder.Services.AddControllers().AddOData(options => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseODataBatching();
app.UseAuthorization();

app.MapControllers();

app.Run();
IEdmModel GetEdmModel() {
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Car>("Cars");
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<CarRental>("CarRentals");
    return builder.GetEdmModel();
}