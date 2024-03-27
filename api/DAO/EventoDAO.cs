using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using MySql.Data.MySqlClient;


namespace api.DAO
{
    public class EventoDAO
    {
        private MySqlConnection _connection;

        public EventoDAO()
        {
            _connection = MySqlConnectionFactory.GetConnection();
        }

         public List<Evento> GetAll()
         {
            List<Evento>  eventos = new List<Evento>();
            string query = "SELECT * FROM eventos";
            
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query, _connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Evento evento = new Evento
                        {
                            IdEvento = reader.GetInt32("id"),
                            Descricao = reader.GetString("descricao"),
                            DataEvento = reader.GetDateTime("data_evento"),
                            TotalIngressos = reader.GetInt32("total_ingressos")
                        };
                            eventos.Add(evento);
                    }
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine($"Erro do Banco: {ex.Message} ");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Erro desconhecido{ex.Message}");
            }
            
            finally
            {
                _connection.Close();
            }   
            return eventos;
        }

        public Evento GetId(int id)
        {
            Evento evento = new Evento();
            string query = $"SELECT * FROM db_evento.evento Where id = {id}";

             try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query, _connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                            evento.IdEvento = reader.GetInt32("id");
                            evento.Descricao = reader.GetString("descricao");
                            evento.DataEvento = reader.GetDateTime("data_evento");
                            evento.TotalIngressos = reader.GetInt32("total_ingressos");
                    }
                }
               
            }

            catch(MySqlException ex)
            {
                Console.WriteLine($"Erro do Banco: {ex.Message} ");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Erro desconhecido{ex.Message}");
            }
            
            finally
            {
                _connection.Close();
            }   
            return evento;
        }

         public void CriarEvento(Evento evento)
         {
            string query = "INSERT INTO evento (id, descricao, data_evento, total_ingressos)" +
            "values(@Id,  @Descricao, @DataEvento, @TotalIngressos)";

            try
            {
                _connection.Open();
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Id", evento.IdEvento);
                    command.Parameters.AddWithValue("@Descricao",evento.Descricao );
                    command.Parameters.AddWithValue("@DataEvento",evento.DataEvento );
                    command.Parameters.AddWithValue("@TotalIngressos",evento.TotalIngressos );
                }

            }
             catch(MySqlException ex)
            {
                Console.WriteLine($"Erro do Banco: {ex.Message} ");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Erro desconhecido{ex.Message}");
            }
            
            finally
            {
                _connection.Close();
            }   
         }

           public void AtualizarEvento(int id, Evento evento)
           {
            string query = "UPDATE evento SET" +
                                    "id=@Id," +
                                    "descricao=@Descricao,"+
                                    "data_evento=@DataEvento,"+
                                    "total_ingressos=@TotalIngressos," + 
                                    "WHERE id=@Id"; 
            try
            {
                _connection.Open();
                 using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Id", evento.IdEvento);
                    command.Parameters.AddWithValue("@Descricao",evento.Descricao );
                    command.Parameters.AddWithValue("@DataEvento",evento.DataEvento );
                    command.Parameters.AddWithValue("@TotalIngressos",evento.TotalIngressos );
                    command.ExecuteNonQuery();
                }
            }
             catch(MySqlException ex)
            {
                Console.WriteLine($"Erro do Banco: {ex.Message} ");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Erro desconhecido{ex.Message}");
            }
            
            finally
            {
                _connection.Close();
            }   

           }

           public void DeleteEvento(int id)
           {
             string query = "DELETE FROM evento WHERE id = @Id";

               try
            {
                _connection.Open();
                using(var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue(@"Id", id);
                    command.ExecuteNonQuery();
                }
            }

            catch(MySqlException ex)
            {
                Console.WriteLine($"Erro do Banco: {ex.Message} ");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Erro desconhecido{ex.Message}");
            }
             finally
            {
                _connection.Close();
            }
           }
     }

        
}
