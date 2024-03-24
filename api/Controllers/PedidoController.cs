namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly PedidoDao _pedidoDao;

    public PedidoController()
    {
        _pedidoDao = new PedidoDao();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var pedidos = _pedidoDao.Get();
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var pedido = _pedidoDao.GetById(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pedido pedido)
    {
        _pedidoDao.Set(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Pedido pedido)
    {
        if (id != pedido.IdPedido) return BadRequest();
        if (_pedidoDao.GetById(id) == null) return NotFound();
        _pedidoDao.Put(pedido);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_pedidoDao.GetById(id) == null) return NotFound();
        _pedidoDao.Delete(id);
        return NoContent();
    }
}