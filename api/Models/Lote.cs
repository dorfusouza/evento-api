namespace api.Models;

public class Lote
{
    [Column("id")]
    public required int IdLote { get; set; }

    [Column("evento_id")]
    public required int EventoId { get; set; }

    [Column("valor_unitario")]
    public required double ValorUnitario { get; set; }

    [Column("quantidade_total")]
    public required int QuantidadeTotal { get; set; }

    [Column("saldo")]
    public required int Saldo { get; set; }

    [Column("ativo")]
    public required int Ativo { get; set; }

    [Column("data_inicio")]
    public DateTime? DataInicio { get; set; }

    [Column("data_final")]
    public DateTime? DataFinal { get; set; }

    [Column("tipo")]
    public required string Tipo { get; set; }

    [Column("nome")]
    public required string Nome { get; set; }
}