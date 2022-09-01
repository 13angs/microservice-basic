using Microsoft.EntityFrameworkCore;
using Plain.RabbitMQ;
using RabbitMQ.Client;
using report_sv.Services;
using report_sv.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
        Title="Report Service",
        Version = "v1",
    });
});

// register the db
builder.Services.AddDbContextPool<ReportContext>(options => {
    options.UseInMemoryDatabase(configuration["Db"]);
});

builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@msb-rabitmq-management:5672"));
builder.Services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
    "report_exchange",
    "report_queue",
    "report.*",
    ExchangeType.Topic
));

builder.Services.AddHostedService<ReportDataCollector>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSwaggerUI();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report Service");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
