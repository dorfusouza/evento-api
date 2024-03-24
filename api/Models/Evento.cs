namespace api.Models;

public class Evento
{
    [Column("id")]
    public required int IdEvento { get; set; }

    [Column("data_evento")]
    public required DateTime DataEvento { get; set; }

    [Column("total_ingressos")]
    public required int TotalIngressos { get; set; }

    [Column("descricao")]
    public string? Descricao { get; set; }
}