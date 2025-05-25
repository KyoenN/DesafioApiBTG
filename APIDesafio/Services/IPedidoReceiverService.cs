using RabbitMQ.Client.Events;

namespace APIDesafio.Services;

public interface IPedidoReceiverService
{
    public Task SetConsumer(AsyncEventingBasicConsumer consumer);
    
}
