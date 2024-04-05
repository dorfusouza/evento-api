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
        var pedidos = _pedidoDao.Read();
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var pedido = _pedidoDao.ReadById(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pedido pedido)
    {
        _pedidoDao.Create(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Pedido pedido)
    {
        if (_pedidoDao.ReadById(pedido.IdPedido) == null) return NotFound();
        _pedidoDao.Update(pedido);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        _pedidoDao.Delete(id);
        return NoContent();
    }
}