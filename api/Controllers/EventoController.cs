using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public class EventoController : ControllerBase
        {
            private EventoDAO _eventoDAO;
        }

        public class EventoController()
        {
            _eventoDAO = new EventoDAO();
        }
    }
}