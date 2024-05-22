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
                Valor = reader.GetDouble("valor"),
                DataUtilizacao = reader.GetDateTime("data_utilizacao"),
                CodigoQr = reader.GetString("codigo_qr"),
                Ativo = reader.GetInt32("ativo")
            };
            ingressos.Add(ingresso);
        }

        return ingressos;
    }

    public List<Ingresso?> Read()
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

    public List<Ingresso?> ReadByPedidoId(int pedidoId)
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

    public List<Ingresso?> ReadByEventoId(int eventoId)
    {
        List<Ingresso?> ingressos;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos " +
                                 "JOIN lote ON ingressos.lote_id = lote.id " +
                                 "WHERE lote.evento_id = @evento_id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@evento_id", eventoId);
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

    public List<Ingresso?> ReadByUsuarioId(int usuarioId)
    {
        List<Ingresso?> ingressos;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos WHERE pedidos_usuarios_id = @usuarios_id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@usuarios_id", usuarioId);
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

    public Ingresso? ReadById(int id)
    {
        Ingresso? ingresso;
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

    public void Create(Ingresso ingresso)
    {
        try
        {
            String codeQR = Guid.NewGuid().ToString();
            ingresso.CodigoQr = codeQR.Substring(0,5) + "@792@" + codeQR.Substring(5);

            _connection.Open();
            const string query = "INSERT INTO ingressos (lote_id, pedidos_id, pedidos_usuarios_id, status, tipo, data_utilizacao, valor, codigo_qr, ativo) " +
                                 "VALUES (@lote_id, @pedidos_id, @pedidos_usuarios_id, @status, @tipo, @data_utilizacao, @valor, @codigo_qr, @ativo)";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", ingresso.LoteId);
            command.Parameters.AddWithValue("@pedidos_id", ingresso.PedidosId);
            command.Parameters.AddWithValue("@pedidos_usuarios_id", ingresso.PedidosUsuariosId);
            command.Parameters.AddWithValue("@status", ingresso.Status);
            command.Parameters.AddWithValue("@tipo", ingresso.Tipo);
            command.Parameters.AddWithValue("@data_utilizacao", ingresso.DataUtilizacao);
            command.Parameters.AddWithValue("@valor", ingresso.Valor);
            command.Parameters.AddWithValue("@codigo_qr", ingresso.CodigoQr);
            command.Parameters.AddWithValue("@ativo", ingresso.Ativo);
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

    public void Update(Ingresso ingresso)
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
                                 "data_utilizacao = @data_utilizacao, " +
                                 "valor = @valor, " +
                                 "codigo_qr = @codigo_qr, " +
                                 "ativo = @ativo " +
                                 "WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", ingresso.LoteId);
            command.Parameters.AddWithValue("@pedidos_id", ingresso.PedidosId);
            command.Parameters.AddWithValue("@pedidos_usuarios_id", ingresso.PedidosUsuariosId);
            command.Parameters.AddWithValue("@status", ingresso.Status);
            command.Parameters.AddWithValue("@tipo", ingresso.Tipo);
            command.Parameters.AddWithValue("@data_utilizacao", ingresso.DataUtilizacao);
            command.Parameters.AddWithValue("@valor", ingresso.Valor);
            command.Parameters.AddWithValue("@codigo_qr", ingresso.CodigoQr);
            command.Parameters.AddWithValue("@ativo", ingresso.Ativo);
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

            const string query = "UPDATE ingressos SET ativo = 0 WHERE id = @id";

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

    public Ingresso? GetIngressoByCodigoQr(string codigo_qr)
    {
        Ingresso? ingresso = null!;

        try
        {
            _connection.Open();
            string query = "SELECT * FROM db_evento.ingressos WHERE codigo_qr = @codigo_qr";

            MySqlCommand command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@codigo_qr", codigo_qr);
            ingresso = ReadAll(command).FirstOrDefault();
        }
        catch (MySqlException ex)
        {
            // Aqui você pode tratar exceções específicas do MySQL
            Console.WriteLine($"Erro ao acessar o banco de dados MySQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Aqui você trata outras exceções gerais
            Console.WriteLine($"Erro desconhecido: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
        return ingresso;
    }

    public List<Ingresso?> GetIngressoByIdUsuario(int usuarioId){
        List<Ingresso?> ingressos;

        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos WHERE pedidos_usuarios_id = @Usuario_Id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Usuario_Id", usuarioId);
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

     public string GetNomeEventoByIdIngresso(int ingressoId){
        string nomeEvento = "";
        try
        {
            _connection.Open();
            const string query = "SELECT evento.nome_evento " +
                             "FROM ingressos " +
                             "JOIN lote ON ingressos.lote_id = lote.id " +
                             "JOIN evento ON lote.evento_id = evento.id " +
                             "WHERE ingressos.id = @Ingresso_Id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Ingresso_Id", ingressoId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    nomeEvento = reader.GetString("nome_evento");
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

        return nomeEvento;
    }

    public List<string> GetAllTiposByIdEvento(int id)
    {
        List<string> tiposIngressos = new List<string>();
        try
        {
            _connection.Open();
            const string query = "SELECT DISTINCT ingressos.tipo " +
                                 "FROM ingressos " +
                                 "JOIN lote ON lote.id = ingressos.lote_id " +
                                 "JOIN evento ON evento.id = lote.evento_id " +
                                 "WHERE evento.id = @IdEvento;";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@IdEvento", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tiposIngressos.Add(reader.GetString("tipo"));
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

        return tiposIngressos;
    }


    public int CountIngressoByTipo(string tipo)
    {
        int quantidadeIngresso = 0;
        try
        {
            _connection.Open();
            const string query = "SELECT COUNT(*) FROM ingressos WHERE tipo = @Tipo;";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Tipo", tipo);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    quantidadeIngresso = reader.GetInt32(0); // Lê o valor da primeira coluna (índice 0) do resultado da consulta
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

        return quantidadeIngresso;
    }

    public void DeleteByLoteId(int loteId)
    {
        try
        {
            _connection.Open();
            const string query = "DELETE FROM ingressos WHERE lote_id = @lote_id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", loteId);
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

    public List<Ingresso?> ReadByLoteId(int loteId)
    {
        List<Ingresso?> ingressos;
        try
        {
            _connection.Open();
            const string query = "SELECT * FROM ingressos WHERE lote_id = @lote_id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@lote_id", loteId);
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

    public void UpdateStatus(int id, string status)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE ingressos SET status = @status WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@status", status);
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

    public void Cancelar(int id, bool action)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE ingressos SET status = 'Cancelado', ativo = @ativo WHERE id = @id";

            //var ativo = action ? 0 : 1;

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ativo", 0);
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

    public Lote GetLoteByIngressoId(int ingressoId)
    {
        Lote lote = null!;
        try
        {
            _connection.Open();
            const string query = "SELECT lote.* FROM ingressos " +
                                 "JOIN lote ON ingressos.lote_id = lote.id " +
                                 "WHERE ingressos.id = @Ingresso_Id";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Ingresso_Id", ingressoId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    lote = new Lote
                    {
                        IdLote = reader.GetInt32("id"),
                        EventoId = reader.GetInt32("evento_id"),
                        ValorUnitario = reader.GetDouble("valor_unitario"),
                        QuantidadeTotal = reader.GetInt32("quantidade_total"),
                        Saldo = reader.GetInt32("saldo"),
                        Ativo = reader.GetInt32("ativo"),
                        DataInicio = reader.GetDateTime("data_inicio"),
                        DataFinal = reader.GetDateTime("data_final"),
                        Tipo = reader.GetString("tipo"),
                        Nome = reader.GetString("nome")
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

        return lote;
    }
}