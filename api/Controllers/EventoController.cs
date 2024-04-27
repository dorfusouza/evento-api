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
        var eventos = _eventoDao.Read();
        return Ok(eventos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var eventos = _eventoDao.ReadById(id);
        if (eventos == null) return NotFound();
        return Ok(eventos);
    }

    [HttpPost]
    public IActionResult Set(Evento evento)
    {
        int id = _eventoDao.Create(evento);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Evento evento)
    {
        if (id != evento.IdEvento) return BadRequest();
        if (_eventoDao.ReadById(id) == null) return NotFound();
        _eventoDao.Update(id, evento);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_eventoDao.ReadById(id) == null) return NotFound();
        _eventoDao.Delete(id);
        return Ok();
    }
}