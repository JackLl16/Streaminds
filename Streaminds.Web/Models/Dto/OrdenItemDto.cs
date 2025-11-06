using System.ComponentModel.DataAnnotations;

namespace Streaminds.Web.Models.Dto
{
 public class OrdenItemDto
 {
 [Required]
 public int ProductoStreamingId { get; set; }

 [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos1")]
 public int Cantidad { get; set; }
 }
}
