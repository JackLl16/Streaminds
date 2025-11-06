using System;
using System.Collections.Generic;

namespace Streaminds.Domain.Entities
{
 public class Cliente
 {
 public int Id { get; set; }
 public string Email { get; set; } = string.Empty;
 public string NombreCompleto { get; set; } = string.Empty;
 public DateTime FechaRegistro { get; set; }
 public bool Activo { get; set; }

 // Navigation
 public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
 public ICollection<AccesoUsuario> Accesos { get; set; } = new List<AccesoUsuario>();
 }
}