using AutoMapper;
using Streaminds.Domain.Entities;
using Streaminds.Web.Models.Dto;

namespace Streaminds.Web.Mapping
{
 public class MappingProfile : Profile
 {
 public MappingProfile()
 {
 CreateMap<ProductoStreaming, ProductoStreamingDto>().ReverseMap();
 CreateMap<Cliente, ClienteDto>().ReverseMap();
 }
 }
}
