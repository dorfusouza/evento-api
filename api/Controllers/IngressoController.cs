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
}