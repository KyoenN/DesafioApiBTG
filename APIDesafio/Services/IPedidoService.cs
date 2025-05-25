using APIDesafio.Models;
using RabbitMQ.Client;

namespace APIDesafio.Services;

public interface IPedidoService
{
    public Task GenerateUniqueId(Pedido pedido);

    public Task SendPedido(Pedido pedido, IChannel channel);
}
