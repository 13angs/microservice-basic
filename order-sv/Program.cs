using Microsoft.EntityFrameworkCore;
using order_sv.Interfaces;
using order_sv.Models;
using order_sv.Services;
using Plain.RabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
        Title="Order Service",
        Version="v1",
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@msb-rabitmq-management:5672"));
builder.Services.AddScoped<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
    "report_exchange",
    ExchangeType.Topic
));

var configuration = builder.Configuration;

builder.Services.AddDbContextPool<OrderContext>(options => {
    options.UseInMemoryDatabase(configuration["Db"]);
});

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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Servce");
});

app.UseAuthorization();

app.MapControllers();

SeedDb.Populate(app);

var factory = new ConnectionFactory{
    Uri=new Uri("amqp://guest:guest@msb-rabitmq-management:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// QueueConsumer.Consume(channel);
// DirectExchangeConsumer.Consume(channel);
// TopicExchangeConsumer.Consume(channel);
// HeaderExchangeConsumer.Consume(channel);
FanoutExchangeConsumer.Consume(channel);

app.Run();
