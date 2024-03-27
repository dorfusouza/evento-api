using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Ingresso
{
    [Column("id")]
    public int IdIngresso {get; set;}

    [Column("pedidos_id")]
    public int PedidosId {get; set;}

    [Column("pedidos_usuarios_id")]
    public int PedidosUsuariosId {get; set;}

    [Column("lote_id")]
    public int LoteId {get; set;}

    [Column("valor")]
    public string? Valor {get; set;}

    [Column("status")]
    public string? Status {get; set;}

    [Column("tipo")]
    public string? Tipo {get; set;}
}