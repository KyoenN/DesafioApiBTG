using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace APIDesafio.Services;

public class PedidoReceiverService : IPedidoReceiverService
{
    private static IChannel? _channel;
    private static AsyncEventingBasicConsumer? consumer;
    public PedidoReceiverService(IChannel channel)
    {
        _channel = channel;
        if (consumer is null)
        {
            consumer = new AsyncEventingBasicConsumer(_channel);
            SetConsumer(consumer);
        }
        
    }

    
    public async Task SetConsumer(AsyncEventingBasicConsumer consumer)
    {
        consumer.ReceivedAsync += async (sender, eventArgs) => {
            byte[] body = eventArgs.Body.ToArray();
            string pedido = Encoding.UTF8.GetString(body);
            PedidoService pedidoToReceive = new();
            await pedidoToReceive.SaveProcessedPedido(pedido);
            Console.WriteLine($"Received Processed: {pedido}");

            await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        };
        await _channel.BasicConsumeAsync("pedidosProcessados", autoAck: false, consumer);
        Console.ReadLine();

    }

    
}
