using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Pedido
{
    [Column("id")]
    public int IdPedido { get; set; }

    [Column("usuarios")]
    public int IdUsuarios  { get; set; }

    [Column("data_cadastro")]
        public DateTime DataCadastro { get; set; }

    [Column("total")]
        public double Total { get; set; }

    [Column("quantidade")]
        public int IdQuantidade { get; set; }

     [Column("forma_pagamento")]
     public string? FormaPagamento { get; set; }

     [Column("status")]
     public string? Status { get; set; }

     [Column("validacao_id_usuarios")]
     public int ValidacaoIdUsuarios { get; set; }


}