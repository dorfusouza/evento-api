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

    [HttpGet("Usuario/{id:int}")]
    public IActionResult GetByUsuarioId(int id)
    {
        var pedidos = _pedidoDao.ReadPedidosByUsuarioId(id);
        if (pedidos == null) return NotFound();
        return Ok(pedidos);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Pedido pedido)
    {
        var newPedido = _pedidoDao.Create(pedido);
        return Ok(newPedido);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Pedido pedido)
    {
        if (id != pedido.IdPedido) return BadRequest();
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        _pedidoDao.Update(pedido);
        return NoContent();
    }

    [HttpPut("validar/{id:int}")]
    public IActionResult PutValidar(int id, int validacaoIdUsuario)
    {
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        var pedido = _pedidoDao.ReadById(id);
        if (pedido == null) return NotFound();
        pedido.ValidacaoIdUsuario = validacaoIdUsuario;
        if (pedido.Status == "Cancelado")
        {
            return BadRequest("Pedido cancelado não pode ser validado");
        }
        _pedidoDao.Validate(pedido);
        return Ok(_pedidoDao.ReadById(id));
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        _pedidoDao.Delete(id);
        return NoContent();
    }
}