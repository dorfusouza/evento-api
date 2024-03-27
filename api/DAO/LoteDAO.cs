namespace api.DAO;

public class LoteDao
{
    private readonly MySqlConnection _connection;

    public LoteDao()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Lote?> ReadAll(MySqlCommand command)
    {
        var lotes = new List<Lote?>();

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) return lotes;
        while (reader.Read())
        {
            var lote = new Lote
            {
                IdLote = reader.GetInt32("id"),
                EventoId = reader.GetInt32("evento_id"),
                ValorUnitario = reader.GetDouble("valor_unitario"),
                QuantidadeTotal = reader.GetInt32("quantidade_total"),
                Saldo = reader.GetInt32("quantidade_total"),
                Ativo = reader.GetInt32("ativo")
            };
            lotes.Add(lote);
        }

        return lotes;
    }

    public List<Lote?> Get()
    {
        List<Lote?> lotes;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM lote";

            var command = new MySqlCommand(query, _connection);
            lotes = ReadAll(command);
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

        return lotes;
    }

    public Lote? GetById(int id)
    {
        Lote? lote;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM lote WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            lote = ReadAll(command).FirstOrDefault();
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

        return lote;
    }

    public void Create(Lote lote)
    {
        try
        {
            _connection.Open();
            const string query = "INSERT INTO lote (evento_id, valor_unitario, quantidade_total, saldo, ativo) " +
                                 "VALUES (@evento_id, @valor_unitario, @quantidade_total, @saldo, @ativo)";

            var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@evento_id", lote.EventoId);
            command.Parameters.AddWithValue("@valor_unitario", lote.ValorUnitario);
            command.Parameters.AddWithValue("@quantidade_total", lote.Saldo);
            command.Parameters.AddWithValue("@saldo", lote.Saldo);
            command.Parameters.AddWithValue("@ativo", lote.Ativo);
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

    public void Update(Lote lote)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE lote SET " +
                                 "evento_id = @evento_id, " +
                                 "valor_unitario = @valor_unitario, " +
                                 "quantidade_total = @quantidade_total, " +
                                 "saldo = @quantidade_total, " +
                                 "ativo = @ativo " +
                                 "WHERE id = @id";

            var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@evento_id", lote.EventoId);
            command.Parameters.AddWithValue("@valor_unitario", lote.ValorUnitario);
            command.Parameters.AddWithValue("@quantidade_total", lote.Saldo);
            command.Parameters.AddWithValue("@saldo", lote.Saldo);
            command.Parameters.AddWithValue("@ativo", lote.Ativo);
            command.Parameters.AddWithValue("@id", lote.IdLote);
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

    public void Delete(int idLote)
    {
        try
        {
            _connection.Open();
            const string query = "DELETE FROM lote WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", idLote);
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