using System.ComponentModel.DataAnnotations;

namespace Streaminds.Web.Models.Dto
{
 public class ProductoStreamingDto
 {
 public int Id { get; set; }
 [Required(ErrorMessage = "El nombre es obligatorio")]
 [StringLength(200, ErrorMessage = "El nombre no puede superar los200 caracteres")]
 public string Nombre { get; set; } = string.Empty;
 [StringLength(1000, ErrorMessage = "La descripción no puede superar los1000 caracteres")]
 public string? Descripcion { get; set; }
 [Required(ErrorMessage = "El tipo de producto es obligatorio")]
 [StringLength(100, ErrorMessage = "El tipo no puede superar los100 caracteres")]
 public string TipoProducto { get; set; } = string.Empty;
 [Range(0,9999999999999999.99, ErrorMessage = "El precio debe ser un valor positivo")]
 public decimal Precio { get; set; }
 [Range(0, int.MaxValue, ErrorMessage = "La duración debe ser un valor no negativo")]
 public int DuracionDias { get; set; }
 [StringLength(50, ErrorMessage = "La calidad no puede superar los50 caracteres")]
 public string CalidadVideo { get; set; } = string.Empty;
 public bool Activo { get; set; }
 }
}
