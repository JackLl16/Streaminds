using System;

namespace Streaminds.Domain.Entities
{
 public class DetalleOrden
 {
 public int Id { get; set; }

 public int OrdenId { get; set; }
 public Orden? Orden { get; set; }

 public int ProductoStreamingId { get; set; }
 public ProductoStreaming? ProductoStreaming { get; set; }

 public int Cantidad { get; set; }
 public decimal PrecioUnitario { get; set; }
 public decimal Subtotal { get; set; }
 }
}