namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngressoController : ControllerBase
{
    private readonly IngressoDao _ingressoDao;
    private readonly LoteDao _loteDao;

    public IngressoController()
    {
        _ingressoDao = new IngressoDao();
        _loteDao = new LoteDao();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var ingressos = _ingressoDao.Read();
        return Ok(ingressos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var ingressos = _ingressoDao.ReadById(id);
        if (ingressos == null) return NotFound();
        return Ok(ingressos);
    }

    [HttpGet("Pedido/{id:int}")]
    public IActionResult GetPedidoById(int id)
    {
        var ingressos = _ingressoDao.ReadByPedidoId(id);
        if (ingressos == null) return NotFound();
        return Ok(ingressos);
    }

    [HttpGet("Usuario/{id:int}")]
    public IActionResult GetUsuarioById(int id)
    {
        var ingressos = _ingressoDao.ReadByUsuarioId(id);
        if (ingressos == null) return NotFound();
        return Ok(ingressos);
    }

    [HttpPost]
    public IActionResult Post([FromBody] List<Ingresso> ingressos)
    {
        var ingressosComprados = 0;
        foreach (var ingresso in ingressos)
        {
            _ingressoDao.Create(ingresso);
            ingressosComprados += 1;
        }

        var lote = _loteDao.GetById(ingressos[0].LoteId);

        if (lote != null)
        {
            lote.Saldo -= ingressosComprados;
            _loteDao.Update(lote);
            _loteDao.UpdateAtivosLotes(lote.EventoId);
        }

        return Ok(ingressos);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Ingresso ingresso)
    {
        if (id != ingresso.IdIngresso) return BadRequest();
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.Update(ingresso);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletarIngresso(int id)
    {
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.Delete(id);
        return Ok();
    }

    [HttpPost("Verifica/{codigo_qr}")]
    public IActionResult GetLoginAsync(string codigo_qr)
    {
        var ingresso = _ingressoDao.GetIngressoByCodigoQr(codigo_qr);

        if (ingresso == null)
        {
            return Unauthorized("Qr Code não Existe");
        }
        else
        {
            if(ingresso.Status == "pendente")
                return Unauthorized("Ingresso não pago!");
            else if(ingresso.Status == "valido")
            {
                ingresso.Status = "utilizado";
                ingresso.DataUtilizacao = DateTime.Now;
                _ingressoDao.Update(ingresso);
                return Ok(new { mensagem = "Acesso concedido!", ingresso }); 
            }
            else
            {
                return Unauthorized(new { mensagem = "Qr Code Inválido!", codigo_qr});
            }
        }
    }

    [HttpGet("quantidadeByTipoByEvento/{id}")]
        public IActionResult GetQuatidadeIngressoByTipoByEvento(int id) // Id do evento desejado
        {
        //var tiposIngressos = _ingressoDao.GetAllTiposByIdEvento(id);
        var tiposIngressos = new List<string>
        {
            "Colaborador",
            "Aluno",
            "Comunidade",
            "Infantil",
        };
        var quantidadePorTipo = new Dictionary<string, int>();
        foreach (var tipo in tiposIngressos)
        {
            var quantidade = _ingressoDao.CountIngressoByTipo(tipo);
            quantidadePorTipo.Add(tipo, quantidade);
        }
        return Ok(new { quantidadePorTipo, ingressos = _ingressoDao.ReadByEventoId(id) });
    }

    [HttpGet("nome/{id:int}")]
    public IActionResult GetNomeEventoByIdIngresso(int id)
    {
        var nome = _ingressoDao.GetNomeEventoByIdIngresso(id);
        if (nome == null) return NotFound();
        return Ok(nome);
    }

    [HttpDelete("deleteByLoteId/{id:int}")]
    public IActionResult DeleteByLoteId(int id)
    {
        if (_ingressoDao.ReadByLoteId(id) == null) return NotFound();
        _ingressoDao.DeleteByLoteId(id);
        return Ok();
    }

    [HttpPut("status/{id:int}")]
    public IActionResult PutStatus(int id, string status)
    {
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.UpdateStatus(id, status);
        return Ok(_ingressoDao.ReadById(id));
    }

    [HttpPut("cancelar/{id:int}")]
    public IActionResult Cancelar(int id)
    {
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.Cancelar(id, true);
        return Ok(_ingressoDao.ReadById(id));
    }
}