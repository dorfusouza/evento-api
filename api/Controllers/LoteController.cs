namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoteController : ControllerBase
{
    private readonly LoteDao _loteDao;
    private readonly EventoDao _eventoDao;
    public LoteController()
    {
        _loteDao = new LoteDao();
        _eventoDao = new EventoDao();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var lotes = _loteDao.Get();
        return Ok(lotes);
    }

    [HttpGet("evento/{id:int}")]
    public IActionResult ReadByEventoId(int id)
    {
        var lotes = _loteDao.GetByEventoId(id);
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
        Lote createdLote = _loteDao.Create(lote);
        _eventoDao.UpdateTotalIngressos(lote.EventoId);
        return Ok(createdLote);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Lote lote)
    {
        if (id != lote.IdLote) return BadRequest();
        if (_loteDao.GetById(id) == null) return NotFound();
        _loteDao.Update(lote);
        _eventoDao.UpdateTotalIngressos(lote.EventoId);
        return Ok(_loteDao.GetById(id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_loteDao.GetById(id) == null) return NotFound();
        var lote = _loteDao.GetById(id);
        _loteDao.Delete(id);
        _eventoDao.UpdateTotalIngressos(lote.EventoId);
        return Ok("Lote deletado com sucesso");
    }

    [HttpDelete("evento/{id:int}")]
    public IActionResult DeleteByEventoId(int id)
    {
        _loteDao.DeleteByEventoId(id);
        _eventoDao.UpdateTotalIngressos(id);
        return Ok();
    }

    [HttpGet("quantidadeIngressos/{id:int}")]
    public IActionResult GetQuantidadeIngressos(int id)
    {
        var quantidade = _loteDao.GetQuantidadeIngressos(id);
        return Ok(quantidade);
    }
}