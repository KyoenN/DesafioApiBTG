
using System.ComponentModel.DataAnnotations;

namespace APIDesafio.Models;

public class Pedido
{

    public int PedidoId { get; set; }
    public int ClienteId { get; set; }
    public List<string>? Items { get; set; }
    public string? Status { get; set; }

}
