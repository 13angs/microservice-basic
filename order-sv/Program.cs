using System.Text;
using Microsoft.EntityFrameworkCore;
using order_sv.Interfaces;
using order_sv.Models;
using order_sv.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IOrderService, OrderService>();

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

app.UseAuthorization();

app.MapControllers();

SeedDb.Populate(app);

var factory = new ConnectionFactory{
    Uri=new Uri("amqp://guest:guest@msb-rabitmq-management:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("demo-queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) => {
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

channel.BasicConsume("demo-queue", true, consumer);

app.Run();
