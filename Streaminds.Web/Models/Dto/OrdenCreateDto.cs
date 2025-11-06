using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Streaminds.Web.Models.Dto
{
 public class OrdenCreateDto
 {
 [Required]
 public int ClienteId { get; set; }
 public List<OrdenItemDto> Items { get; set; } = new List<OrdenItemDto>();
 }
}
