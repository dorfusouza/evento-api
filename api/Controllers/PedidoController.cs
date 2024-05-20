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
        
        var ingressos = _ingressoDao.ReadByPedidoId(pedido.IdPedido);
        if (ingressos == null || !ingressos.Any()) return NotFound();

        if (pedido.Status == "Validado")
        {
            foreach (var ingresso in ingressos)
            {
                _ingressoDao.UpdateStatus(ingresso.IdIngresso, "Validado");
            }

        } else if (pedido.Status == "Pendente")
        {
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
    public IActionResult Cancelar(int id, int cancelamentoIdUsuario)
    {
        var pedido = _pedidoDao.ReadById(id);
        if (pedido == null) return NotFound();

        pedido.ValidacaoIdUsuario = cancelamentoIdUsuario;
        _pedidoDao.Cancelar(pedido);
        pedido = _pedidoDao.ReadById(id);

        var ingressos = _ingressoDao.ReadByPedidoId(pedido.IdPedido);
        if (ingressos == null || !ingressos.Any()) return NotFound();

        var lote = _ingressoDao.GetLoteByIngressoId(ingressos[0].IdIngresso);
        if (lote == null) return NotFound();

        // Aqui estamos cancelando os ingressos e atualizando o saldo do lote
        if (pedido.Status == "Pendente" || pedido.Status == "Validado")
        {
            foreach (var ingresso in ingressos)
            {
                _ingressoDao.Cancelar(ingresso.IdIngresso, true);
            }

            lote.Saldo -= pedido.Quantidade;
            _loteDao.Update(lote);

        // Aqui estamos reativando os ingressos e atualizando o saldo do lote
        } else if (pedido.Status == "Cancelado")
        {
            foreach (var ingresso in ingressos)
            {
                _ingressoDao.Cancelar(ingresso.IdIngresso, false);
            }
            lote.Saldo += pedido.Quantidade;
            _loteDao.Update(lote);
        }

        _loteDao.UpdateAtivosLotes(lote.EventoId);

        return Ok(pedido);
    }
}