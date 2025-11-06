using System;

namespace Streaminds.Domain.Entities
{
 public class AccesoUsuario
 {
 public int Id { get; set; }

 public int ClienteId { get; set; }
 public Cliente? Cliente { get; set; }

 public int ProductoStreamingId { get; set; }
 public ProductoStreaming? ProductoStreaming { get; set; }

 public DateTime FechaActivacion { get; set; }
 public DateTime FechaExpiracion { get; set; }
 }
}