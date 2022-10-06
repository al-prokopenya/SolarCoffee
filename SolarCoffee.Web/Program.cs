using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System.Configuration;
using SolarCoffee.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SolarDbContext>(opts => {
    opts.EnableDetailedErrors();
    //opts.UseInMemoryDatabase("demo");
    //opts.use
    //opts.UseSqlServer(builder.Configuration.GetConnectionString("mssql.dev"));
    //opts.UseNpgsql(builder.Configuration.GetConnectionString("solar.dev"));
    opts.UseSqlite(builder.Configuration.GetConnectionString("sqlite.dev"));
});

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrderService, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://solar-coffee.vercel.app", "http://localhost:8080")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder=>builder
    .WithOrigins("https://solar-coffee.vercel.app", "http://localhost:8080")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());
}


//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();
