namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly EventoDao _eventoDao;

    public EventoController()
    {
        _eventoDao = new EventoDao();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var eventos = _eventoDao.Get();
        return Ok(eventos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var eventos = _eventoDao.GetById(id);
        if (eventos == null) return NotFound();
        return Ok(eventos);
    }

    [HttpPost]
    public IActionResult Set(Evento evento)
    {
        _eventoDao.CriarEvento(evento);
        return CreatedAtAction(nameof(GetById), new { id = evento.IdEvento }, evento);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Evento evento)
    {
        if (id != evento.IdEvento) return BadRequest();
        var exists = _eventoDao.GetById(id);
        if (exists == null) return NotFound();
        _eventoDao.AtualizarEvento(id, evento);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_eventoDao.GetById(id) == null) return NotFound();
        _eventoDao.DeleteEvento(id);
        return Ok();
    }
}