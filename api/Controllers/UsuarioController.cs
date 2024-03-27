using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DAO;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioControllers : ControllerBase
    {
        private UsuarioDAO _usuarioDAO;

        public UsuarioControllers(){
            _usuarioDAO = new UsuarioDAO();
        }

        [HttpGet]
        public IActionResult GetAllUsuarios(){
            var usuarios = _usuarioDAO.GetAll();
            return Ok(usuarios);
        } 

        [HttpGet("{id}")]
        public IActionResult GetUsuarioId(int id){
            var usuario = _usuarioDAO.GetId(id);
            if (usuario  == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }     

        [HttpPost]
        public IActionResult CreateUsuario(Usuario usuario)
        {
            _usuarioDAO.Create(usuario);
            return Ok();            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuarioAtualizado){
            if(_usuarioDAO.GetId(id) == null){
                return NotFound();
            }
            _usuarioDAO.Update(id, usuarioAtualizado);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id){
            if(_usuarioDAO.GetId(id) == null){
                return NotFound();
            }
            _usuarioDAO.Delete(id);
            return Ok();
        }     
    }
}