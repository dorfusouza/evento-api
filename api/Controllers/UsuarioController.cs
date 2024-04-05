namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioDao _usuarioDao;

    public UsuarioController()
    {
        _usuarioDao = new UsuarioDao();
    }

    [HttpGet]
    public IActionResult Get()
    {
        var usuarios = _usuarioDao.Get();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var usuario = _usuarioDao.GetById(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public IActionResult Post(Usuario usuario)
    {
        _usuarioDao.Create(usuario);
        return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut]
    public IActionResult Put(Usuario usuario)
    {
        if (_usuarioDao.GetById(usuario.IdUsuario) == null) return NotFound();
        _usuarioDao.Update(usuario);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_usuarioDao.GetById(id) == null) return NotFound();
        _usuarioDao.Delete(id);
        return NoContent();
    }
}