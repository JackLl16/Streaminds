using System.ComponentModel.DataAnnotations;

namespace Streaminds.Web.Models.Dto
{
 public class ClienteDto
 {
 public int Id { get; set; }
 [Required(ErrorMessage = "El email es obligatorio")]
 [EmailAddress(ErrorMessage = "Email no válido")]
 public string Email { get; set; } = string.Empty;
 [Required(ErrorMessage = "El nombre completo es obligatorio")]
 [StringLength(200, ErrorMessage = "El nombre no puede superar los200 caracteres")]
 public string NombreCompleto { get; set; } = string.Empty;
 public DateTime FechaRegistro { get; set; }
 public bool Activo { get; set; }
 }
}
