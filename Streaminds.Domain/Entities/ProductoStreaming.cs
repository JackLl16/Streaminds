using System;
using System.Collections.Generic;

namespace Streaminds.Domain.Entities
{
 public class ProductoStreaming
 {
 public int Id { get; set; }
 public string Nombre { get; set; } = string.Empty;
 public string? Descripcion { get; set; }
 public string TipoProducto { get; set; } = string.Empty;
 public decimal Precio { get; set; }
 public int DuracionDias { get; set; }
 public string CalidadVideo { get; set; } = string.Empty;
 public bool Activo { get; set; }

 // Navigation
 public ICollection<DetalleOrden> DetallesOrden { get; set; } = new List<DetalleOrden>();
 public ICollection<AccesoUsuario> AccesosUsuarios { get; set; } = new List<AccesoUsuario>();
 }
}