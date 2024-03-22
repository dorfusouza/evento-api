using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using MySql.Data.MySqlClient;

namespace api.DAO
{
    public class UsuarioDAO{
        private MySqlConnection _connection;

        public UsuarioDAO(){
            _connection = MySqlConnectionFactory.GetConnection();
        }

        public List<Usuario> GetAll(){
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT * FROM usuarios";
            try{
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query, _connection);
                using(MySqlDataReader reader =  command.ExecuteReader()){
                    while(reader.Read()){
                        Usuario usuario = new Usuario();
                        usuario.IdUsuario = reader.GetInt32("id");
                        usuario.NomeCompleto = reader.GetString("nome_completo");
                        usuario.Senha = reader.GetSenha("senha");
                        usuario.Email = reader.GetString("email");
                        usuario.Telefone = reader.GetInt32("telefone");
                        usuario.IsAtivo = reader.GetBool("status");
                        usuario.Perfil = reader.GetString("perfil");
                        usuarios.Add(usuario);
                    }
                }

            }catch(MySqlException ex){
                Console.WriteLine($"Erro do BANCO: {ex.Message}");
            }catch(Exception ex){
                Console.WriteLine($"ERRO DESCONHECIDO: {ex.Message}");
            }finally{
                _connection.Close();
            }
            return usuarios;
        }

        public Usuario GetId(int id){
            Usuario usuario = new Usuario();
            string query = $"SELECT * FROM usuarios WHERE id = {id}";
            try{
                _connection.Open();
                MySqlCommand command = new MySqlCommand(query, _connection);
                using(MySqlDataReader reader = command.ExecuteReader()){   
                    while(reader.Read()){
                        usuario.IdUsuario = reader.GetInt32("id");
                        usuario.NomeCompleto = reader.GetString("nome_completo");
                        usuario.Senha = reader.GetInt32("senha");
                        usuario.Email = reader.GetString("email");
                        usuario.Telefone = reader.GetInt32("telefone");
                        usuario.IsAtivo = reader.GetBool("status");
                        usuario.Perfil = reader.GetString("perfil");
                    }
                }
            }catch(MySqlException ex){
                Console.WriteLine($"Erro no Banco: {ex.Message}");
            }catch(Exception ex){
                Console.WriteLine($"Erro Desconhecido: {ex.Message}");
            }
            finally{
                _connection.Close();
            }
            return usuario;
        }

        public void Create(Usuario usuario){
            string query = $"INSERT INTO usuarios (nome_completo, email, senha, telefone, perfil, status) values (@NomeCompleto, @Email, @Senha, @Telefone, @Perfil, @Status);";
            try{
                _connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, _connection)){
                    command.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
                    command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                    command.Parameters.AddWithValue("@Perfil", usuario.Perfil);
                    command.Parameters.AddWithValue("@Status", usuario.IsAtivo);
                    command.ExecuteNonQuery();
                }
            }catch(MySqlException ex){
                Console.WriteLine($"Erro no Banco: {ex.Message}");
            }catch(Exception ex){
                Console.WriteLine($"Erro Desconhecido: {ex.Message}");
            }
            finally{
                _connection.Close();
            }
        }

        public void Update(int id, Usuario usuario){
            string query= $"UPDATE usuarios SET nome_completo=@NomeCompleto, Email=@Email, senha=@Senha, telefone=@Telefone, perfil=@Perfil, status=@Status WHERE id_personagem = {id}";
            try{
            _connection.Open();
            using ( MySqlCommand command = new MySqlCommand(query, _connection)){
                command.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);
                command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                command.Parameters.AddWithValue("@Perfil", usuario.Perfil);
                command.Parameters.AddWithValue("@Status", usuario.IsAtivo);
                command.ExecuteNonQuery();
            }
                
            }catch(MySqlException ex){
                Console.WriteLine($"Erro no Banco: {ex.Message}");
            }catch(Exception ex){
                Console.WriteLine($"Erro Desconhecido: {ex.Message}");
            }finally{
                _connection.Close();
            }
        }
        
        public void Delete(int id){ 
            string query = $"DELETE FROM usuarios WHERE id = {id}";
            try{
                _connection.Open();
                using ( MySqlCommand command = new MySqlCommand(query, _connection)){
                    command.ExecuteNonQuery();
                }
            }catch(MySqlException ex){
                Console.WriteLine($"Erro no Banco: {ex.Message}");
            }catch(Exception ex){
                Console.WriteLine($"Erro Desconhecido: {ex.Message}");
            }
            finally{
                _connection.Close();
            }
        }
        
    }
}