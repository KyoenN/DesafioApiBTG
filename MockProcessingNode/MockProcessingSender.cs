using RabbitMQ.Client;
using System.Text;

namespace MockProcessingNode;

class MockProcessingSender
{ 
    public MockProcessingSender(IChannel _channel,string pedido)
    {
        SendProcessedPedido(pedido,_channel);
    }

    public async Task SendProcessedPedido(string pedido,IChannel _channel)
    {
        string statusOld = "Pendente";
        string statusNew = "Processado";
        
        pedido = pedido.Replace(statusOld, statusNew);

        var body = Encoding.UTF8.GetBytes(pedido);
        await Task.Delay(15000);
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "pedidosProcessados", mandatory: true,
        basicProperties: new BasicProperties { Persistent = true }, body: body);
        Console.WriteLine($"Sent Processed: {pedido}");
        Console.ReadLine();
    }    
}
