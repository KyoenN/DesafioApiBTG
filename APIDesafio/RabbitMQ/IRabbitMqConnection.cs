using RabbitMQ.Client;

namespace APIDesafio.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }

        IChannel Channel { get; }
    }
}
