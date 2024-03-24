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
        var ingressos = _ingressoDao.Get();
        return Ok(ingressos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var ingressos = _ingressoDao.GetById(id);
        if (ingressos == null) return NotFound();
        return Ok(ingressos);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Ingresso ingresso)
    {
        _ingressoDao.Set(ingresso);
        return CreatedAtAction(nameof(GetById), new { id = ingresso.IdIngresso }, ingresso);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Ingresso ingresso)
    {
        if (id != ingresso.IdIngresso) return BadRequest();
        var exists = _ingressoDao.GetById(id);
        if (exists == null) return NotFound();
        _ingressoDao.Put(ingresso);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletarIngresso(int id)
    {
        var ingresso = _ingressoDao.GetById(id);
        if (ingresso == null) return NotFound();
        _ingressoDao.Delete(id);
        return NoContent();
    }
}