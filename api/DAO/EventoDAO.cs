namespace api.DAO;

public class EventoDao
{
    private readonly MySqlConnection _connection;

    public EventoDao()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Evento?> ReadAll(MySqlCommand command)
    {
        var eventos = new List<Evento?>();

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) return eventos;
        while (reader.Read())
        {
            var evento = new Evento
            {
                IdEvento = reader.GetInt32("id"),
                Descricao = reader.GetString("descricao"),
                DataEvento = reader.GetDateTime("data_evento"),
                TotalIngressos = reader.GetInt32("total_ingressos"),
                ImagemUrl = reader.GetString("imagem_url"),
                Local = reader.GetString("local"),
                Ativo = reader.GetInt32("ativo")
            };
            eventos.Add(evento);
        }

        return eventos;
    }

    public List<Evento?> Read()
    {
        List<Evento?> eventos = null!;

        try
        {
            _connection.Open();
            const string query = "SELECT * FROM evento";

            var command = new MySqlCommand(query, _connection);

            eventos = ReadAll(command);
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do Banco: {ex.Message} ");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido{ex.Message}");
        }

        finally
        {
            _connection.Close();
        }

        return eventos;
    }

    public Evento? ReadById(int id)
    {
        Evento? evento = null!;

        try
        {
            _connection.Open();
            var query = "SELECT * FROM db_evento.evento Where id = @Id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            evento = ReadAll(command).FirstOrDefault();
        }

        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do Banco: {ex.Message} ");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido{ex.Message}");
        }

        finally
        {
            _connection.Close();
        }

        return evento;
    }

    public void Create(Evento evento)
    {
        try
        {
            _connection.Open();
            const string query = "INSERT INTO evento (id, descricao, data_evento, total_ingresso, imagem_url, local, ativo) " +
                                 "VALUES(@Id,  @Descricao, @DataEvento, @TotalIngressos, @ImagemUrl, @Local, @Ativo)";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", evento.IdEvento);
            command.Parameters.AddWithValue("@Descricao", evento.Descricao);
            command.Parameters.AddWithValue("@DataEvento", evento.DataEvento);
            command.Parameters.AddWithValue("@TotalIngressos", evento.TotalIngressos);
            command.Parameters.AddWithValue("@ImagemUrl", evento.ImagemUrl);
            command.Parameters.AddWithValue("@Local", evento.Local);
            command.Parameters.AddWithValue("@Ativo", evento.Ativo);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do Banco: {ex.Message} ");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido{ex.Message}");
        }

        finally
        {
            _connection.Close();
        }
    }

    public void Update(int id, Evento evento)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE evento SET " +
                                 "descricao = @Descricao, " +
                                 "data_evento = @DataEvento, " +
                                 "total_ingressos = @TotalIngressos, " +
                                 "imagem_url = @ImagemUrl, " +
                                 "local = @Local, " +
                                 "ativo = @Ativo " +
                                 "WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@Id", evento.IdEvento);
            command.Parameters.AddWithValue("@Descricao", evento.Descricao);
            command.Parameters.AddWithValue("@DataEvento", evento.DataEvento);
            command.Parameters.AddWithValue("@TotalIngressos", evento.TotalIngressos);
            command.Parameters.AddWithValue("@ImagemUrl", evento.ImagemUrl);
            command.Parameters.AddWithValue("@Local", evento.Local);
            command.Parameters.AddWithValue("@Ativo", evento.Ativo);
            
            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do Banco: {ex.Message} ");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido{ex.Message}");
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
            const string query = "DELETE FROM evento WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue(@"Id", id);
            command.ExecuteNonQuery();
        }

        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do Banco: {ex.Message} ");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido{ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
}