using System.ComponentModel.DataAnnotations;

namespace Streaminds.Web.Models.Dto
{
 public class RegisterDto
 {
 [Required(ErrorMessage = "El email es obligatorio")]
 [EmailAddress(ErrorMessage = "Email no válido")]
 public string Email { get; set; } = string.Empty;
 [Required(ErrorMessage = "La contraseña es obligatoria")]
 [StringLength(100, MinimumLength =6, ErrorMessage = "La contraseña debe tener al menos6 caracteres")]
 [DataType(DataType.Password)]
 public string Password { get; set; } = string.Empty;
 [DataType(DataType.Password)]
 [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
 public string ConfirmPassword { get; set; } = string.Empty;
 }
}
