using System.ComponentModel.DataAnnotations.Schema;

namespace api.Repository;

public class Usuario
{
    [Column ("id")]
    public int IdUsuario { get; set; }

    [Column ("nome_completo")]
    public string NomeCompleto { get; set; }

    [Column ("email")]
    public string Email { get; set; } 

    [Column ("senha")]
    public string Senha { get; set; }

    [Column ("telefone")]
    public int Telefone { get; set; }

    [Column ("perfil")]
    public string Perfil { get; set; }

    [Column ("status")]
    public bool IsAtivo { get; set; }
}