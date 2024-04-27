namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoteController : ControllerBase
{
    private readonly LoteDao _loteDao;
    private readonly IngressoDao _ingressoDao;
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
        if (lote.EventoId == 0) return BadRequest();
        if (lote.DataFinal == null) lote.DataFinal = DateTime.Now;
        if (lote.DataInicio == null) lote.DataInicio = DateTime.Now;
        _loteDao.Create(lote);

        return Ok(lote.EventoId);
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