namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngressoController : ControllerBase
{
    private readonly IngressoDao _ingressoDao;

    public IngressoController()
    {
        _ingressoDao = new IngressoDao();
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
    public IActionResult Post([FromBody] Ingresso ingresso)
    {
        _ingressoDao.Create(ingresso);
        return CreatedAtAction(nameof(GetById), new { id = ingresso.IdIngresso }, ingresso);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Ingresso ingresso)
    {
        if (id != ingresso.IdIngresso) return BadRequest();
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.Update(ingresso);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletarIngresso(int id)
    {
        if (_ingressoDao.ReadById(id) == null) return NotFound();
        _ingressoDao.Delete(id);
        return NoContent();
    }

    

    [HttpPost("Verifica/{codigo_qr}")]
    public IActionResult GetLoginAsync(string codigo_qr)
    {
        var ingresso = _ingressoDao.GetIngressoByCodigoQR(codigo_qr);

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
}