using Microsoft.EntityFrameworkCore;
using Plain.RabbitMQ;
using product_sv.Interfaces;
using product_sv.Models;
using product_sv.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
        Title="Product Service",
        Version="v1"
    });
});

var configuration = builder.Configuration;

builder.Services.AddDbContextPool<ProductContext>(options => {
    options.UseInMemoryDatabase(configuration["Db"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@msb-rabitmq-management:5672"));
builder.Services.AddScoped<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
    "report_exchange",
    ExchangeType.Topic
));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service");
});

app.UseAuthorization();

app.MapControllers();

// SeedDb.Populate(app);

app.Run();
