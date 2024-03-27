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
    public IActionResult Read()
    {
        var lotes = _loteDao.Get();
        return Ok(lotes);
    }

    [HttpGet("{id:int}")]
    public IActionResult ReadById(int id)
    {
        var lote = _loteDao.GetById(id);
        if (lote == null) return NotFound();
        return Ok(lote);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Lote lote)
    {
        _loteDao.Create(lote);
        return CreatedAtAction(nameof(ReadById), new { id = lote.IdLote }, lote);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Lote lote)
    {
        if (id != lote.IdLote) return BadRequest();
        if (_loteDao.GetById(id) == null) return NotFound();
        _loteDao.Update(lote);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_loteDao.GetById(id) == null) return NotFound();
        _loteDao.Delete(id);
        return NoContent();
    }
}