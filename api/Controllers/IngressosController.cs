using api.DAO;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class IngressoController : ControllerBase
    {
        private IngressoDAO _ingressoDAO;

        public IngressoController()
        {
            _ingressoDAO = new IngressoDAO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ingressos = _ingressoDAO.GetAll();
            return Ok(ingressos);
        }

        [HttpGet("{id}")] //passando o id para o get
        public IActionResult GetById(int id) //interpretando o id
        {
            var personagem = _ingressoDAO.GetId(id); //busca o id no personagem

            if(id == null)  //se nao encontrar o id, ele retorna o erro NotFOund
            {
                return NotFound(); 
            }
            else{
                return Ok(personagem); //se encontrar o personagem, ele retorna o OK.
            }
        }

        [HttpPost]
        public IActionResult CriarPersonagem(Ingresso ingresso)
        {
            _ingressoDAO.CreateIngresso(ingresso);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPersonagem(int id, Ingresso ingresso)
        {
            if(_ingressoDAO.GetId(id) == null)
            {
                return NotFound();
            }

            _ingressoDAO.AtualizarIngresso(id, ingresso);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarPersonagem(int id)
        {
            if(_ingressoDAO.GetId(id) == null)
            {
                return NotFound();
            }

            _ingressoDAO.DeletarIngresso(id);
            return Ok();
        }
    }
}