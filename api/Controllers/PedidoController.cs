using api.DAO;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private PedidoDAO _pedidoDAO;

    public PedidoController()
    {
        _pedidoDAO = new PedidoDAO();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var pedidos = _pedidoDAO.Get();
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var pedido = _pedidoDAO.GetById(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return Ok(pedido);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pedido pedido)
    {
        _pedidoDAO.Set(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Pedido pedido)
    {
        if (id != pedido.IdPedido)
        {
            return BadRequest();
        }
        var exists = _pedidoDAO.GetById(id);
        if (exists == null)
        {
            return NotFound();
        }
        _pedidoDAO.Put(pedido);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var exists = _pedidoDAO.GetById(id);
        if (exists == null)
        {
            return NotFound();
        }
        _pedidoDAO.Delete(id);
        return NoContent();
    }
}