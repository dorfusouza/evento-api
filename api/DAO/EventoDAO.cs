using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repository;
using MySql.Data.MySqlClient;


namespace api.EventoDAO
{
    public class EventoDAO
    {
        private MySqlConnection _connection;


        public class EventoDAO()
        {
            _connection = MySqlConnectionFactory.GetConnection();
        }

        public List<Evento> GetAll()
        {
            List<Evento> eventos = new List<Evento>();
        }
    }
}