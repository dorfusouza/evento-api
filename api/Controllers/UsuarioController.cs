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
    public IActionResult Read()
    {
        var usuarios = _usuarioDao.Get();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public IActionResult ReadById(int id)
    {
        var usuario = _usuarioDao.GetById(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public IActionResult Post(Usuario usuario)
    {
        _usuarioDao.Create(usuario);
        return CreatedAtAction(nameof(ReadById), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.IdUsuario) return BadRequest();
        if (_usuarioDao.GetById(id) == null) return NotFound();
        _usuarioDao.Update(id, usuario);
        return Ok(_usuarioDao.GetById(id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUsuario(int id)
    {
        if (_usuarioDao.GetById(id) == null) return NotFound();
        _usuarioDao.Delete(id);
        return NoContent();
    }
}