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
                NomeEvento = reader.GetString("nome_evento"),
                ImagemUrl = reader.GetString("imagem_url"),
                TotalIngressos = reader.GetInt32("total_ingressos"),
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

    public int Create(Evento evento)
    {
        int id = 0;
        try
        {
            _connection.Open();
            const string query = "INSERT INTO evento (id, descricao, data_evento, nome_evento, imagem_url, local, ativo, total_ingressos) " +
                                 "VALUES(@Id,  @Descricao, @DataEvento, @NomeEvento, @ImagemUrl, @Local, @Ativo, @TotalIngressos)";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", evento.IdEvento);
            command.Parameters.AddWithValue("@Descricao", evento.Descricao);
            command.Parameters.AddWithValue("@DataEvento", evento.DataEvento);
            command.Parameters.AddWithValue("@NomeEvento", evento.NomeEvento);
            command.Parameters.AddWithValue("@ImagemUrl", evento.ImagemUrl);
            command.Parameters.AddWithValue("@Local", evento.Local);
            command.Parameters.AddWithValue("@Ativo", evento.Ativo);
            command.Parameters.AddWithValue("@TotalIngressos", evento.TotalIngressos);

            command.ExecuteNonQuery();
            id = (int)command.LastInsertedId;
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
        return id;
    }

    public void Update(int id, Evento evento)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE evento SET " +
                                 "descricao = @Descricao, " +
                                 "data_evento = @DataEvento, " +
                                 "nome_evento = @NomeEvento, " +
                                 "imagem_url = @ImagemUrl, " +
                                 "local = @Local, " +
                                 "ativo = @Ativo, " +
                                 "total_ingressos = @TotalIngressos " +
                                 "WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@Descricao", evento.Descricao);
            command.Parameters.AddWithValue("@DataEvento", evento.DataEvento);
            command.Parameters.AddWithValue("@NomeEvento", evento.NomeEvento);
            command.Parameters.AddWithValue("@ImagemUrl", evento.ImagemUrl);
            command.Parameters.AddWithValue("@Local", evento.Local);
            command.Parameters.AddWithValue("@Ativo", evento.Ativo);
            command.Parameters.AddWithValue("@TotalIngressos", evento.TotalIngressos);
            command.Parameters.AddWithValue("@Id", id);

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