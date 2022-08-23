using Microsoft.EntityFrameworkCore;
using product_sv.Interfaces;
using product_sv.Models;
using product_sv.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContextPool<ProductContext>(options => {
    options.UseInMemoryDatabase(configuration["Db"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

SeedDb.Populate(app);

app.Run();
