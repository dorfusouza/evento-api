namespace api.DAO;

public class IngressoDao
{
    private readonly MySqlConnection _connection;

    public IngressoDao()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Ingresso?> ReadAll(MySqlCommand command)
    {
        var ingressos = new List<Ingresso?>();

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) return ingressos;
        while (reader.Read())
        {
            var ingresso = new Ingresso
            {
                IdIngresso = reader.GetInt32("id"),
                LoteId = reader.GetInt32("lote_id"),
                PedidosId = reader.GetInt32("pedidos_id"),
                PedidosUsuariosId = reader.GetInt32("pedidos_usuarios_id"),
                Status = reader.GetString("status"),
                Tipo = reader.GetString("tipo"),
                Valor = reader.GetDouble("valor")
            };
            ingressos.Add(ingresso);
        }

        return ingressos;
    }

    public List<Ingresso?> Get()
    {
        List<Ingresso?> ingressos;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos";
            var command = new MySqlCommand(query, _connection);
            ingressos = ReadAll(command);
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

        return ingressos;
    }

    public List<Ingresso?> GetByPedidoId(int pedidoId)
    {
        List<Ingresso?> ingressos;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos WHERE pedidos_id = @pedidos_id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@pedidos_id", pedidoId);
            ingressos = ReadAll(command);
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

        return ingressos;
    }

    public Ingresso? GetById(int id)
    {
        Ingresso? ingresso = null!;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            ingresso = ReadAll(command).FirstOrDefault();
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

        return ingresso;
    }

    public void Set(Ingresso ingresso)
    {
        try
        {
            _connection.Open();
            const string query = "INSERT INTO ingressos (lote_id, pedidos_id, pedidos_usuarios_id, status, tipo, valor) " +
                                 "VALUES (@lote_id, @pedidos_id, @pedidos_usuarios_id, @status, @tipo, @valor)";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", ingresso.LoteId);
            command.Parameters.AddWithValue("@pedidos_id", ingresso.PedidosId);
            command.Parameters.AddWithValue("@pedidos_usuarios_id", ingresso.PedidosUsuariosId);
            command.Parameters.AddWithValue("@status", ingresso.Status);
            command.Parameters.AddWithValue("@tipo", ingresso.Tipo);
            command.Parameters.AddWithValue("@valor", ingresso.Valor);
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

    public void Put(Ingresso ingresso)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE ingressos SET " +
                                 "lote_id = @lote_id, " +
                                 "pedidos_id = @pedidos_id, " +
                                 "pedidos_usuarios_id = @pedidos_usuarios_id, " +
                                 "status = @status, " +
                                 "tipo = @tipo, " +
                                 "valor = @valor " +
                                 "WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", ingresso.LoteId);
            command.Parameters.AddWithValue("@pedidos_id", ingresso.PedidosId);
            command.Parameters.AddWithValue("@pedidos_usuarios_id", ingresso.PedidosUsuariosId);
            command.Parameters.AddWithValue("@status", ingresso.Status);
            command.Parameters.AddWithValue("@tipo", ingresso.Tipo);
            command.Parameters.AddWithValue("@valor", ingresso.Valor);
            command.Parameters.AddWithValue("@id", ingresso.IdIngresso);
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

            const string query = "DELETE FROM ingressos WHERE id = @id";

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