using System.Text;
using RabbitMQ.Client;
using APIDesafio.Controllers;

namespace APIDesafio.RabbitMQ;

public class RabbitMqConnection : IRabbitMqConnection
{
    private readonly IConnection? _connection;
    private readonly IChannel? _channel;
    public IConnection Connection => _connection!;
    public IChannel Channel => _channel!;

    public RabbitMqConnection(IConnection Connection, IChannel Channel)
    {
        _connection = Connection;
        _channel = Channel;
        DeclareQueue(_channel);
    }

    private async void DeclareQueue(IChannel channel)
    {
        await channel.QueueDeclareAsync(queue: "pedidos", durable: true, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueDeclareAsync(queue: "pedidosProcessados", durable: true, exclusive: false, autoDelete: false, arguments: null);
    } 
}
