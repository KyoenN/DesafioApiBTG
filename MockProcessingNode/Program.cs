using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using MockProcessingNode;

var factory = new ConnectionFactory { HostName = "localhost" };
using var _connection = await factory.CreateConnectionAsync();
using var _channel = await _connection.CreateChannelAsync();
await _channel.QueueDeclareAsync(queue: "pedidos", durable: true, exclusive: false, autoDelete: false, arguments: null);
await _channel.QueueDeclareAsync(queue: "pedidosProcessados", durable: true, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new AsyncEventingBasicConsumer(_channel);
    consumer.ReceivedAsync += async (sender, eventArgs) =>
    {
        byte[] body = eventArgs.Body.ToArray();
        string pedido = Encoding.UTF8.GetString(body);

        await Task.Delay(1000);
        MockProcessingSender pedidoSender = new(_channel,pedido);

        Console.WriteLine($"Received Unprocessed: {pedido}");

        await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
    };
    await _channel.BasicConsumeAsync("pedidos", autoAck: false, consumer);
    Console.ReadLine();


    
