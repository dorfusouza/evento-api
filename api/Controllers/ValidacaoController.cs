namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidacaoController : ControllerBase
{
    private readonly IngressoDao _ingressoDAO;

    public ValidacaoController()
    {
        _ingressoDAO = new IngressoDao();
    }

    [HttpGet("Verifica/{codigo_qr}")]
    public IActionResult GetIngressoByCodigoQR(string codigo_qr)
    {
        var ingresso = _ingressoDAO.GetIngressoByCodigoQr(codigo_qr);
        if (ingresso == null)
            return NotFound();
        else
            return Ok(ingresso);
    }
    

    [HttpPost]
    public IActionResult ValidaIngresso(Ingresso ingresso)
    {

        //Se o ingresso estiver ativo, ele é desativado e retorna a mensagem de que foi validado com sucesso
        if (ingresso.Status == "ATIVO")
        {
            ingresso.Status = "VALIDADO";
            ingresso.DataUtilizacao = DateTime.Now;
            _ingressoDAO.Update(ingresso);
            return Ok("Ingresso validado com sucesso!");
        } else {
            return Ok("Ingresso indisponivel ou já foi validado!");
        }
    }

}
