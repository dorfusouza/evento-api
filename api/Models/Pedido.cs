namespace api.Models;

public class Pedido
{
    [Column("id")]
    public required int IdPedido { get; set; }

    [Column("usuarios_id")]
    public int? UsuariosId { get; set; }

    [Column("data")]
    public required DateTime DataCadastro { get; set; }

    [Column("total")]
    public required double Total { get; set; }

    [Column("quantidade")]
    public required int Quantidade { get; set; }

    [Column("forma_pagamento")]
    public required string FormaPagamento { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("validacao_id_usuarios")]
    public int? ValidacaoIdUsuario { get; set; }
}