namespace api.Repository;

public class Usuario
{
    [Column("id")] public required int IdUsuario { get; set; }

    [Column("nome_completo")] public required string NomeCompleto { get; set; }

    [Column("email")] public required string Email { get; set; }

    [Column("senha")] public required string Senha { get; set; }

    [Column("telefone")] public required int Telefone { get; set; }

    [Column("perfil")] public string? Perfil { get; set; }

    [Column("status")] public required bool IsAtivo { get; set; }
}