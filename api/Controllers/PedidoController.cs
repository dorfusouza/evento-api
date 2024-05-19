namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly PedidoDao _pedidoDao;
    private readonly IngressoDao _ingressoDao;
    private readonly LoteDao _loteDao;

    public PedidoController()
    {
        _pedidoDao = new PedidoDao();
        _ingressoDao = new IngressoDao();
        _loteDao = new LoteDao();
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
        pedido = _pedidoDao.ReadById(id);
        if (pedido.Status == "Validado")
        {
            var ingressos = _ingressoDao.ReadByPedidoId(pedido.IdPedido);
            foreach (var ingresso in ingressos)
            {
                _ingressoDao.UpdateStatus(ingresso.IdIngresso, "Validado");
            }
        } else if (pedido.Status == "Pendente")
        {
            var ingressos = _ingressoDao.ReadByPedidoId(pedido.IdPedido);
            foreach (var ingresso in ingressos)
            {
                _ingressoDao.UpdateStatus(ingresso.IdIngresso, "Pendente");
            }
        }
        return Ok(pedido);
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        _pedidoDao.Delete(id);
        return NoContent();
    }

    [HttpPut("cancelar/{id:int}")]
    public IActionResult Cancelar(int id, int validacaoIdUsuario)
    {
        if (_pedidoDao.ReadById(id) == null) return NotFound();
        var pedido = _pedidoDao.ReadById(id);
        if (pedido == null) return NotFound();
        pedido.ValidacaoIdUsuario = validacaoIdUsuario;
        _pedidoDao.Cancelar(pedido);
        // Cancela todos os ingressos e retorna o saldo para o lote
        var ingressos = _ingressoDao.ReadByPedidoId(pedido.IdPedido);
        foreach (var ingresso in ingressos)
        {
            var lote = _ingressoDao.GetLoteByIngressoId(ingresso.IdIngresso);
            // Se o ingresso já foi cancelado, ele reverte o cancelamento
            if (ingresso.Ativo == 0)
            {
                _ingressoDao.UpdateStatus(ingresso.IdIngresso, "Pendente");
                _loteDao.UpdateSaldo(lote.IdLote, 1);
                _ingressoDao.Cancelar(ingresso.IdIngresso, false);
                continue;
            } 
            _ingressoDao.Cancelar(ingresso.IdIngresso, true);
            _loteDao.UpdateSaldo(lote.IdLote, -1);
        }

        return Ok(_pedidoDao.ReadById(id));
    }
}