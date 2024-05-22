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
            return File(System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "imagens", "default.png")), "image/png");
        
        }

        var image = System.IO.File.OpenRead(path);
        return File(image, "image/png");
    }

    [HttpPost]
    public IActionResult Set(Evento evento)
    {
        _eventoDao.Create(evento);
        _eventoDao.UpdateTotalIngressos(_eventoDao.Read().Last().IdEvento);
        return Ok(_eventoDao.Read().Last());
    }

    [HttpPut]
    public IActionResult Update(Evento evento)
    {
        if (_eventoDao.ReadById(evento.IdEvento) == null) return NotFound();
        _eventoDao.Update(evento);
        _eventoDao.UpdateTotalIngressos(evento.IdEvento);
        return Ok(_eventoDao.ReadById(evento.IdEvento));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_eventoDao.ReadById(id) == null) return NotFound();
        if (_loteDao.CheckExists(id)) {
            var lotes = _loteDao.GetByEventoId(id);
            foreach (var lote in lotes)
            {
                var loteId = lote.IdLote;
                _ingressoDao.DeleteByLoteId(loteId);
                System.Console.WriteLine($"Lote {loteId} deletado");;
            }
            System.Console.WriteLine($"Lotes deletados");
            _loteDao.DeleteByEvento(id);
        }
        _eventoDao.Delete(id);
        System.Console.WriteLine($"Evento {id} deletado");
        return Ok("Evento deletado com sucesso");
    }
}