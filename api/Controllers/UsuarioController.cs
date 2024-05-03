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
        return NoContent();
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
        // Verifica se as credenciais foram fornecidas corretamente
        if (credentials == null || string.IsNullOrEmpty(credentials.email) || string.IsNullOrEmpty(credentials.senha))
        {
            return BadRequest("Credenciais inválidas");
        }

        // Busca o usuário no banco de dados pelo e-mail
        var usuario = _usuarioDao.GetEmail(credentials.email);

        // Verifica se o usuário foi encontrado
        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        // Verifica se a senha fornecida corresponde à senha do usuário no banco de dados
        if (usuario.Senha != credentials.senha)
        {
            return Unauthorized("Senha incorreta");
        }

        // Se chegou até aqui, as credenciais são válidas e o usuário pode ser autenticado
        return Ok(usuario);
    }

}

