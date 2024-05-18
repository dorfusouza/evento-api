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
        //Verifica se o email já está cadastrado
        if (_usuarioDao.GetEmail(usuario.Email) != null)
        {
            return BadRequest("Email já cadastrado");
        }
        _usuarioDao.Create(usuario);

        return Ok(_usuarioDao.Get().Last());
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
    [HttpPost("login")]
    public IActionResult GetLoginAsync([FromBody] UsuarioCredenciais credentials)
    {
        if (credentials == null || string.IsNullOrEmpty(credentials.email) || string.IsNullOrEmpty(credentials.senha))
        {
            return BadRequest("Credenciais inválidas");
        }

        var usuario = _usuarioDao.GetEmail(credentials.email);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        if (usuario.Senha != credentials.senha)
        {
            return Unauthorized("Senha incorreta");
        }

        return Ok(usuario);
    }

}

