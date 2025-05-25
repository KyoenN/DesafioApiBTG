using APIDesafio.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace APIDesafio.Services;

public class PedidoService : IPedidoService
{
    private static IChannel? _channel;
    public IChannel Channel => _channel!;
    private static int idSeed;
    private static List<string>? pedidosList;

    public PedidoService(IChannel channel)
    {
        _channel = channel;
        pedidosList = new List<string>();
    }

    public PedidoService()
    {

    }

    public async Task GenerateUniqueId(Pedido pedido)
    {
        idSeed++;
        pedido.PedidoId += idSeed;
        await GenerateStatusTag(pedido);
    }

    public async Task GenerateStatusTag(Pedido pedido)
    {
        pedido.Status = "Pendente";
        await SendPedido(pedido, _channel);
    }

    public async Task SendPedido(Pedido pedido, IChannel _channel)
    {
        string jsonPedido = JsonSerializer.Serialize(pedido);
        var body = Encoding.UTF8.GetBytes(jsonPedido);
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "pedidos", mandatory: true,
        basicProperties: new BasicProperties { Persistent = true }, body: body);
        Console.WriteLine($"Sent Unprocessed: {jsonPedido}");
        await PushIntoList(jsonPedido);
    }

    public async Task PushIntoList(string jsonPedido)
    {
        pedidosList.Add(jsonPedido);
    }
    public async Task<string> GetById(int id)
    {
        string jsonGetPedido = pedidosList.ElementAt(id - 1);

        if (jsonGetPedido == null)
        {
            return string.Empty;
        }
        return jsonGetPedido;
    }
    public async Task SaveProcessedPedido(string pedido)
    {       
        Pedido _pedido = JsonSerializer.Deserialize<Pedido>(pedido);

        int id = _pedido.PedidoId;
        pedidosList[id - 1] = pedido;
        
    }
}
