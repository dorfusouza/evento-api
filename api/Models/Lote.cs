using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Lote
{
    [Column("id")]
    public int IdLote { get; set; }

    [Column("evento_id")]
    public int EventoId { get; set; }

    [Column("valor_unitario")]
    public double ValorUnitario { get; set; }

    [Column("quantidade_total")]
    public int Saldo { get; set; }
}

