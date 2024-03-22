using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using MySql.Data.MySqlClient;

namespace api.DAO
{
    public class IngressoDAO
    {
        private MySqlConnection _connection;


        public IngressoDAO()
        {
            _connection = MySqlConnectionFactory.GetConnection();
        }

        public List<Ingresso> GetAll()
        {
            List<Ingresso> ingressos = new List<Ingresso>();
            string query = "SELECT * FROM ingressos";

            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query, _connection);
                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Ingresso ingresso = new Ingresso();
                        ingresso.IdIngresso = reader.GetInt32("id");
                        ingresso.PedidosId = reader.GetInt32("pedidos_id");
                        ingresso.PedidosUsuariosId= reader.GetInt32("pedidos_usuarios_id");
                        ingresso.LoteId= reader.GetInt32("lote_id");
                        ingresso.Valor= reader.GetString("valor");
                        ingresso.Status = reader.GetString("status");
                        ingresso.Tipo= reader.GetString("tipo");

                        ingressos.Add(ingresso);
                                                
                    }
                }
            }
            catch(MySqlException ex)
            {
                //mapeando os erros do banco!!!
                Console.WriteLine($"Erro do BANCO: {ex.Message}");
            }

            catch(Exception ex)
            {
                //mapeando os erros de forma geral!!!
                Console.WriteLine($"Erro Desconhecido: {ex.Message}");
            }
            finally
            {
                _connection.Close(); //fechando a conexão com o banco!!!
            }

            return ingressos; //retornando a lista!!!
        }

        public Ingresso GetId(int id)
        {
            Ingresso ingresso = new Ingresso();

            string query = $"SELECT * FROM ingressos WHERE id = {id}";

            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query,_connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        ingresso.IdIngresso = reader.GetInt32("id");
                        ingresso.PedidosId = reader.GetInt32("pedidos_id");
                        ingresso.PedidosUsuariosId= reader.GetInt32("pedidos_usuarios_id");
                        ingresso.LoteId= reader.GetInt32("lote_id");
                        ingresso.Valor= reader.GetString("valor");
                        ingresso.Status = reader.GetString("status");
                        ingresso.Tipo= reader.GetString("tipo");                 
                    }
                }
            }

            catch(MySqlException ex)
            {
                Console.WriteLine($"ERRO NO BANCO: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO DESCONHECIDO: {ex.Message}");
            }
            finally
            {
                _connection.Close(); //fechando a conexão
            }

            return ingresso;
        }

        public void CreateIngresso(Ingresso ingresso)
        {
            string query = $"INSERT INTO ingressos (id, pedidos_id, pedidos_usuarios_id, lote_id, valor_ status, tipo)" + 
            "VALUES (@IdIngresso, @PedidosId, @PedidosUsuariosId, @LoteId, @Valor, @Status, @Tipo)";

            try
            {
                _connection.Open();
                using(var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@IdIngresso", ingresso.IdIngresso);
                    command.Parameters.AddWithValue("@PedidosId", ingresso.PedidosId);
                    command.Parameters.AddWithValue("@PedidosUsuariosId", ingresso.PedidosUsuariosId);
                    command.Parameters.AddWithValue("@LoteId", ingresso.LoteId);
                    command.Parameters.AddWithValue("@Valor", ingresso.Valor);
                    command.Parameters.AddWithValue("@Status", ingresso.Status);
                    command.Parameters.AddWithValue("@Status", ingresso.Status);
                    command.Parameters.AddWithValue("@Tipo", ingresso.Tipo);
                    command.ExecuteNonQuery();
                }
            }

            catch(MySqlException ex)
            {
                Console.WriteLine($"Erro de BANCO: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro de BANCO: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }            
        }

        public void AtualizarIngresso(int id, Ingresso ingresso)
        {
            string query = "UPDATE ingressos SET" +
                            "IdIngresso=@IdIngresso,    "+
                            "PedidosId=@PedidosId,  "+
                            "PedidosUsuariosId=@PedidosUsuariosId,  "+
                            "LoteId=@LoteId,    "+
                            "Valor=@Valor,  "+
                            "Status=@Status, "+
                            "Tipo=@Tipo";

        try
        {
            _connection.Open();
            using(var command = new MySqlCommand(query, _connection))
            {
                    command.Parameters.AddWithValue("@IdIngresso", ingresso.IdIngresso);
                    command.Parameters.AddWithValue("@PedidosId", ingresso.PedidosId);
                    command.Parameters.AddWithValue("@PedidosUsuariosId", ingresso.PedidosUsuariosId);
                    command.Parameters.AddWithValue("@LoteId", ingresso.LoteId);
                    command.Parameters.AddWithValue("@Valor", ingresso.Valor);
                    command.Parameters.AddWithValue("@Status", ingresso.Status);
                    command.Parameters.AddWithValue("@Status", ingresso.Status);
                    command.Parameters.AddWithValue("@Tipo", ingresso.Tipo);
                    command.ExecuteNonQuery();
            }
        }
            catch(MySqlException ex)
            {
                Console.WriteLine($"ERRO DE BANCO: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO DESCONHECIDO: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

        }

        public void DeletarIngresso(int id)
        {
            string query="DELETE FROM ingressos WHERE id = @IdIngresso";

            try
            {
                _connection.Open();
                using(var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@IdIngresso", id);
                    command.ExecuteNonQuery();
                }
            }

            catch(MySqlException ex)
            {
                Console.WriteLine($"ERRO DE BANCO: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO DESCONHECIDO: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}