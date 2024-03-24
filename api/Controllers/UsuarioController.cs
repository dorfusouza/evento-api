namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioControllers : ControllerBase
{
    private readonly UsuarioDao _usuarioDao;

    public UsuarioControllers()
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
        _usuarioDao.Set(usuario);
        return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.IdUsuario) return BadRequest();
        if (_usuarioDao.GetById(id) == null) return NotFound();
        _usuarioDao.Put(id, usuario);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUsuario(int id)
    {
        if (_usuarioDao.GetById(id) == null) return NotFound();
        _usuarioDao.Delete(id);
        return NoContent();
    }
}