namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly EventoDao _eventoDao;
    private readonly LoteDao _loteDao;
    private readonly IngressoDao _ingressoDao;
    private readonly PedidoDao _pedidoDao;

    public EventoController()
    {
        _eventoDao = new EventoDao();
        _loteDao = new LoteDao();
        _ingressoDao = new IngressoDao();
        _pedidoDao = new PedidoDao();
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

    [HttpGet("{id}/image")]
    public IActionResult GetEventoImage(int id)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "imagens", $"{id}.png");
        if (!System.IO.File.Exists(path))
        {
            return NotFound();
        }

        var image = System.IO.File.OpenRead(path);
        return File(image, "image/png");
    }

    [HttpPost]
    public IActionResult Set(Evento evento)
    {
        _eventoDao.Create(evento);
        return Ok(_eventoDao.Read().Last());
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
        if (_loteDao.CheckExists(id)) {
            _ingressoDao.DeleteByLoteId(id);

            //Iremos deletar todos os lotes
            _loteDao.DeleteByEvento(id);
        }
        _eventoDao.Delete(id);
        return Ok();
    }
}