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
                Saldo = reader.GetInt32("saldo"),
                Ativo = reader.GetInt32("ativo"),
                DataFinal = reader.GetDateTime("data_final"),
                DataInicio = reader.GetDateTime("data_inicio"),
                Tipo = reader.GetString("tipo"),
                Nome = reader.GetString("nome")
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

    public List<Lote?> GetByEventoId(int id)
    {
        List<Lote?> lotes;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM lote WHERE evento_id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

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

    public Lote Create(Lote lote)
    {
        Lote createdLote = lote;
        try
        {
            _connection.Open();
            const string query = "INSERT INTO lote (evento_id, valor_unitario, quantidade_total, saldo, ativo, data_final, data_inicio, tipo, nome) " +
                                 "VALUES (@evento_id, @valor_unitario, @quantidade_total, @saldo, @ativo, @data_final, @data_inicio, @tipo, @nome)";

            var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@evento_id", lote.EventoId);
            command.Parameters.AddWithValue("@valor_unitario", lote.ValorUnitario);
            command.Parameters.AddWithValue("@quantidade_total", lote.QuantidadeTotal);
            command.Parameters.AddWithValue("@saldo", lote.Saldo);
            command.Parameters.AddWithValue("@ativo", lote.Ativo);
            command.Parameters.AddWithValue("@data_final", lote.DataFinal);
            command.Parameters.AddWithValue("@data_inicio", lote.DataInicio);
            command.Parameters.AddWithValue("@tipo", lote.Tipo);
            command.Parameters.AddWithValue("@nome", lote.Nome);

            command.ExecuteNonQuery();
            createdLote.IdLote = (int)command.LastInsertedId;
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
        return createdLote;
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
                                 "saldo = @saldo, " +
                                 "ativo = @ativo, " +
                                 "data_final = @data_final, " +
                                 "data_inicio = @data_final, " +
                                 "tipo = @tipo, " +
                                 "nome = @nome " +
                                 "WHERE id = @id";

            var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@evento_id", lote.EventoId);
            command.Parameters.AddWithValue("@valor_unitario", lote.ValorUnitario);
            command.Parameters.AddWithValue("@quantidade_total", lote.QuantidadeTotal);
            command.Parameters.AddWithValue("@saldo", lote.Saldo);
            command.Parameters.AddWithValue("@ativo", lote.Ativo);
            command.Parameters.AddWithValue("@data_final", lote.DataFinal);
            command.Parameters.AddWithValue("@data_inicio", lote.DataInicio);
            command.Parameters.AddWithValue("@tipo", lote.Tipo);
            command.Parameters.AddWithValue("@nome", lote.Nome);
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

    public void DeleteByEventoId(int idEvento)
    {
        try
        {
            _connection.Open();
            const string query = "DELETE FROM lote WHERE evento_id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", idEvento);
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

    public List<List<int>> GetQuantidadeIngressos(int id)
    {
        List<List<int>> quantidadeIngressos = new();
        try
        {
            _connection.Open();
            const string query = "SELECT lote.id, lote.quantidade_total, lote.saldo, COUNT(ingressos.id) as vendidos " +
                                 "FROM lote " +
                                 "LEFT JOIN ingressos ON lote.id = ingressos.lote_id " +
                                 "WHERE lote.evento_id = @id " +
                                 "GROUP BY lote.id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (!reader.HasRows) return quantidadeIngressos;
            while (reader.Read())
            {
                var lote = new List<int>
                {
                    reader.GetInt32("id"),
                    reader.GetInt32("quantidade_total"),
                    reader.GetInt32("saldo"),
                    reader.GetInt32("vendidos"),
                };
                quantidadeIngressos.Add(lote);
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

        return quantidadeIngressos;
    }
}