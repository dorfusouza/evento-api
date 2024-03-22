using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.DAO;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private EventoDAO _eventoDAO;

        public EventoController()
        {
            _eventoDAO = new EventoDAO();

        }
         public IActionResult Get()
        {
            var eventos = _eventoDAO.GetAll();
            return Ok(eventos);
        }

         [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
           var eventos = _eventoDAO.GetId(id);
            if( eventos == null)
            {
                return NotFound();
            }
            return Ok(eventos);
        }

         [HttpPost]
        public IActionResult CriarEvento(Evento evento)
        {
            _eventoDAO.CriarEvento(evento);
            return Ok();
        }

                [HttpPost("{id}")]
        public IActionResult AtualizarEvento(int id, Evento evento)
        {
            if(_eventoDAO.GetId(id) == null)
            {
                return NotFound();
            }
            _eventoDAO.AtualizarEvento(id, evento);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarEvento(int id)
        {
            if(_eventoDAO.GetId(id) == null)
            {
                return NotFound();
            }
            _eventoDAO.DeleteEvento(id);
            return Ok();
        }
    }
}