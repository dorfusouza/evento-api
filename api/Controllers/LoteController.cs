namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoteController : ControllerBase
{
    private readonly LoteDao _loteDao;

    public LoteController()
    {
        _loteDao = new LoteDao();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var lotes = _loteDao.Get();
        return Ok(lotes);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var lote = _loteDao.GetById(id);
        if (lote == null) return NotFound();
        return Ok(lote);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Lote lote)
    {
        _loteDao.Set(lote);
        return CreatedAtAction(nameof(GetById), new { id = lote.IdLote }, lote);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Lote lote)
    {
        if (id != lote.IdLote) return BadRequest();
        var exists = _loteDao.GetById(id);
        if (exists == null) return NotFound();
        _loteDao.Put(lote);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var lote = _loteDao.GetById(id);
        if (lote == null) return NotFound();
        _loteDao.Delete(lote.IdLote);
        return NoContent();
    }
}