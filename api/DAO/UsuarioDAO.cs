namespace api.DAO;

public class UsuarioDao
{
    private readonly MySqlConnection _connection;

    public UsuarioDao()
    {
        _connection = MySqlConnectionFactory.GetConnection();
    }

    private static List<Usuario?> ReadAll(MySqlCommand command)
    {
        var usuarios = new List<Usuario?>();

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) return usuarios;
        while (reader.Read())
        {
            var usuario = new Usuario
            {
                IdUsuario = reader.GetInt32("id"),
                NomeCompleto = reader.GetString("nome_completo"),
                Senha = reader.GetString("senha"),
                Email = reader.GetString("email"),
                Telefone = reader.GetString("telefone"),
                Ativo = reader.GetInt32("ativo"),
                Perfil = reader.GetString("perfil")
            };
            usuarios.Add(usuario);
        }

        return usuarios;
    }

    public List<Usuario?> Get()
    {
        var usuarios = new List<Usuario?>();

        try
        {
            _connection.Open();
            const string query = "SELECT * FROM usuarios";
            var command = new MySqlCommand(query, _connection);
            usuarios = ReadAll(command);
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro do BANCO: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO DESCONHECIDO: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }

        return usuarios;
    }

    public Usuario? GetById(int id)
    {
        Usuario? usuario = null!;

        try
        {
            _connection.Open();
            const string query = "SELECT * FROM usuarios WHERE id = @id";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            usuario = ReadAll(command).FirstOrDefault();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro no Banco: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro Desconhecido: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }

        return usuario;
    }

    public void Create(Usuario usuario)
    {
        try
        {
            _connection.Open();


            const string query = "INSERT INTO usuarios (nome_completo, email, senha, telefone, perfil, ativo)" +
                                 "VALUES (@nome_completo, @email, @senha, @telefone, @perfil, @ativo)";

            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nome_completo", usuario.NomeCompleto);
            command.Parameters.AddWithValue("@email", usuario.Email);
            command.Parameters.AddWithValue("@senha", usuario.Senha);
            command.Parameters.AddWithValue("@telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@perfil", usuario.Perfil);
            command.Parameters.AddWithValue("@ativo", usuario.Ativo);
            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro no Banco: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro Desconhecido: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Update(int id, Usuario usuario)
    {
        try
        {
            _connection.Open();
            const string query = "UPDATE usuarios SET " +
                                 "nome_completo = @nome_completo, " +
                                 "email = @email, " +
                                 "senha = @senha, " +
                                 "telefone = @telefone, " +
                                 "perfil = @perfil, " +
                                 "ativo = @ativo " +
                                 "WHERE id = @id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nome_completo", usuario.NomeCompleto);
            command.Parameters.AddWithValue("@email", usuario.Email);
            command.Parameters.AddWithValue("@senha", usuario.Senha);
            command.Parameters.AddWithValue("@telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@perfil", usuario.Perfil);
            command.Parameters.AddWithValue("@ativo", usuario.Ativo);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro no Banco: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro Desconhecido: {ex.Message}");
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
            const string query = "UPDATE usuarios SET ativo = 0 WHERE id = @id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro no Banco: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro Desconhecido: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    public Usuario? GetEmail(string email)
    {
        Usuario? usuario = null;

        string query = $"SELECT * FROM usuarios WHERE email = '{email}'";
        try
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand(query, _connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = reader.GetInt32("id"),
                        NomeCompleto = reader.GetString("nome_completo"),
                        Senha = reader.GetString("senha"),
                        Email = reader.GetString("email"),
                        Telefone = reader.GetString("telefone"),
                        Ativo = reader.GetInt32("ativo"),
                        Perfil = reader.GetString("perfil")
                    };
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro no banco: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro desconhecido: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
        return usuario;
    }
}