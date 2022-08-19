using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System.Configuration;
using SolarCoffee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SolarDbContext>(opts => {
    opts.EnableDetailedErrors();
    opts.UseNpgsql(builder.Configuration.GetConnectionString("solar.dev"));
});

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrderService, OrderService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
