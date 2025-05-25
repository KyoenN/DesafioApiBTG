using APIDesafio.RabbitMQ;
using APIDesafio.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


// Garantindo que a conexao nao vai dar autodispose precoce
var factory = new ConnectionFactory { HostName = "localhost" };
using var _connection = await factory.CreateConnectionAsync();
using var _channel = await _connection.CreateChannelAsync();

// Add services to the container
builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection(_connection,_channel));
builder.Services.AddSingleton<IPedidoService>(new PedidoService(_channel));
builder.Services.AddSingleton<IPedidoReceiverService>(new PedidoReceiverService(_channel));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
