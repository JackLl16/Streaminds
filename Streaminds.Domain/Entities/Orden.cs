using System;
using System.Collections.Generic;

namespace Streaminds.Domain.Entities
{
 public class Orden
 {
 public int Id { get; set; }
 public DateTime Fecha { get; set; }

 public int ClienteId { get; set; }
 public Cliente? Cliente { get; set; }

 public decimal Total { get; set; }
 public string Estado { get; set; } = string.Empty;

 // Navigation
 public ICollection<DetalleOrden> DetalleOrdenes { get; set; } = new List<DetalleOrden>();
 }
}