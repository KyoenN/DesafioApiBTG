using APIDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using APIDesafio.Services;

namespace APIDesafio.Controllers;

[Route("pedidos")]
[ApiController]
public class PedidoController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Post(Pedido pedido)
    {
        if (pedido is null)
            return BadRequest();
        
        PedidoService sendPedido = new();
        await sendPedido.GenerateUniqueId(pedido);

        return new CreatedAtRouteResult("pedidos", new { id = pedido.PedidoId }, pedido); 
    }

    [HttpGet("{id:int}", Name = "pedidos")]
    public async Task<ActionResult<string>> Get(int id)
    {
        PedidoService GetPedido = new();
        string returnGet = await GetPedido.GetById(id);

        if (returnGet == "" || returnGet == null)
        {
            return NotFound();
        }
        return Ok(returnGet);
    }
}
