namespace api.Models;

public class Ingresso
{
    [Column("id")]
    public required int IdIngresso { get; set; }

    [Column("pedidos_id")]
    public required int PedidosId { get; set; }

    [Column("pedidos_usuarios_id")]
    public required int PedidosUsuariosId { get; set; }

    [Column("lote_id")]
    public required int LoteId { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("tipo")]
    public required string Tipo { get; set; }

    [Column("valor")]
    public required double Valor { get; set; }

    [Column("data_utilizacao")]
    public DateTime? DataUtilizacao { get; set; }

    [Column("codigo_qr")]
    public string? CodigoQr { get; set; }

    [Column("ativo")]
    public int? Ativo { get; set; }
}