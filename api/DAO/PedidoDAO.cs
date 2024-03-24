namespace api.DAO;

public class PedidoDao
{
    private readonly MySqlConnection _connection;

    public PedidoDao()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Pedido?> ReadAll(MySqlCommand command)
    {
        var pedidos = new List<Pedido?>();

        using var reader = command.ExecuteReader();
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
                ValidacaoIdUsuario = reader.GetInt32("validacao_id_usuario")
            };
            pedidos.Add(pedido);
        }

        return pedidos;
    }

    public List<Pedido?> Get()
    {
        var pedidos = new List<Pedido?>();
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM pedidos";
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
        Pedido? pedido = null!;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM pedidos WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            pedido = ReadAll(command).FirstOrDefault();
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
        const string query = "INSERT INTO pedidos (usuarios_id, data, total, quantidade, forma_pagamento, status, validacao_id_usuario) " +
                             "values (@usuarios_id, @data_cadastro, @total, @quantidade, @forma_pagamento, @status, @validacao_id_usuario)";
        try
        {
            _connection.Open();
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@usuarios_id", pedido.UsuariosId);
            command.Parameters.AddWithValue("@data_cadastro", pedido.DataCadastro);
            command.Parameters.AddWithValue("@total", pedido.Total);
            command.Parameters.AddWithValue("@Quantidade", pedido.Quantidade);
            command.Parameters.AddWithValue("@forma_pagamento", pedido.FormaPagamento);
            command.Parameters.AddWithValue("@status", pedido.Status);
            command.Parameters.AddWithValue("@validacao_id_usuario", pedido.ValidacaoIdUsuario);
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
        try
        {
            _connection.Open();
            const string query = "UPDATE pedidos SET " +
                                    "usuarios_id = @usuarios_id, " +
                                    "data = @data_cadastro, " +
                                    "total = @total, " +
                                    "quantidade = @quantidade, " +
                                    "forma_pagamento = @forma_pagamento, " +
                                    "status = @status, " +
                                    "validacao_id_usuario = @validacao_id_usuario " +
                                    "WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@usuarios_id", pedido.UsuariosId);
            command.Parameters.AddWithValue("@data_cadastro", pedido.DataCadastro);
            command.Parameters.AddWithValue("@total", pedido.Total);
            command.Parameters.AddWithValue("@Quantidade", pedido.Quantidade);
            command.Parameters.AddWithValue("@forma_pagamento", pedido.FormaPagamento);
            command.Parameters.AddWithValue("@status", pedido.Status);
            command.Parameters.AddWithValue("@validacao_id_usuario", pedido.ValidacaoIdUsuario);
            command.Parameters.AddWithValue("@id", pedido.IdPedido);
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
        try
        {
            _connection.Open();

            var query = "DELETE FROM pedido WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
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