using api.Models;
using api.Repository;
using MySql.Data.MySqlClient;

namespace api.DAO;

public class PedidoDAO
{
    private MySqlConnection _connection;

    public PedidoDAO()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Pedido> ReadAll(MySqlCommand command)
    {
        var pedidos = new List<Pedido>();

        using (var reader = command.ExecuteReader())
        {
            if (!reader.HasRows) return pedidos;
            while (reader.Read())
            {
                var pedido = new Pedido
                {
                    IdPedido = reader.GetInt32("id"),
                    UsuariosId = reader.GetInt32("usuarios_id"),
                    DataCadastro = reader.GetDateTime("data"),
                    Total = reader.GetDouble("total"),
                    Quantidade = reader.GetInt32("quantidade"),
                    FormaPagamento = reader.GetString("forma_pagamento"),
                    Status = reader.GetString("status"),
                    ValidacaoIdUsuarios = reader.GetInt32("validacao_id_usuario")
                };
                pedidos.Add(pedido);
            }
        }

        return pedidos;
    }

    public List<Pedido> Get()
    {
        var pedidos = new List<Pedido>();
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM pedido";
            var command = new MySqlCommand(query, _connection);
            pedidos = ReadAll(command);
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
        return pedidos;
    }

    public Pedido? GetById(int id)
    {
        Pedido pedido = null;
        try
        {
            _connection.Open();
            var query = $"SELECT * FROM pedido WHERE id = {id}";
            var command = new MySqlCommand(query, _connection);
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows) return null;
                while (reader.Read())
                {
                    pedido = new Pedido
                    {
                        IdPedido = reader.GetInt32("id"),
                        UsuariosId = reader.GetInt32("usuarios_id"),
                        DataCadastro = reader.GetDateTime("data"),
                        Total = reader.GetDouble("total"),
                        Quantidade = reader.GetInt32("quantidade"),
                        FormaPagamento = reader.GetString("forma_pagamento"),
                        Status = reader.GetString("status"),
                        ValidacaoIdUsuarios = reader.GetInt32("validacao_id_usuario")
                    };
                }
            }
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
        return pedido;
    }

    public void Set(Pedido pedido)
    {
        var query = $"INSERT INTO pedido (usuarios_id, data, total, quantidade, forma_pagamento, status, validacao_id_usuario) values (@UsuariosId, @DataCadastro, @Total, @Quantidade, @FormaPagamento, @Status, @ValidacaoIdUsuarios);";
        try
        {
            _connection.Open();
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UsuariosId", pedido.UsuariosId);
            command.Parameters.AddWithValue("@DataCadastro", pedido.DataCadastro);
            command.Parameters.AddWithValue("@Total", pedido.Total);
            command.Parameters.AddWithValue("@Quantidade", pedido.Quantidade);
            command.Parameters.AddWithValue("@FormaPagamento", pedido.FormaPagamento);
            command.Parameters.AddWithValue("@Status", pedido.Status);
            command.Parameters.AddWithValue("@ValidacaoIdUsuarios", pedido.ValidacaoIdUsuarios);
            command.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Put(Pedido pedido)
    {
        var query = $"UPDATE pedido SET usuarios_id = @UsuariosId, data = @DataCadastro, total = @Total, quantidade = @Quantidade, forma_pagamento = @FormaPagamento, status = @Status, validacao_id_usuario = @ValidacaoIdUsuarios WHERE id = {pedido.IdPedido}";
        try
        {
            _connection.Open();
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UsuariosId", pedido.UsuariosId);
            command.Parameters.AddWithValue("@DataCadastro", pedido.DataCadastro);
            command.Parameters.AddWithValue("@Total", pedido.Total);
            command.Parameters.AddWithValue("@Quantidade", pedido.Quantidade);
            command.Parameters.AddWithValue("@FormaPagamento", pedido.FormaPagamento);
            command.Parameters.AddWithValue("@Status", pedido.Status);
            command.Parameters.AddWithValue("@ValidacaoIdUsuarios", pedido.ValidacaoIdUsuarios);
            command.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        var query = $"DELETE FROM pedido WHERE id = {id}";
        try
        {
            _connection.Open();
            var command = new MySqlCommand(query, _connection);
            command.ExecuteNonQuery();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

}
