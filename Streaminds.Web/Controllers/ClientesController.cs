using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Streaminds.Domain.Contracts;
using Streaminds.Web.Models.Dto;
using X.PagedList;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Streaminds.Web.Controllers
{
 [Authorize]
 public class ClientesController : Controller
 {
 private readonly IUnitOfWork _uow;
 private readonly IMapper _mapper;
 private const int PageSize =10;
 public ClientesController(IUnitOfWork uow, IMapper mapper)
 {
 _uow = uow;
 _mapper = mapper;
 }

 public async Task<IActionResult> Index(string? search, int page=1)
 {
 var all = await _uow.Clientes.GetAll();
 if(!string.IsNullOrWhiteSpace(search)) all = all.Where(c => c.NombreCompleto.Contains(search, System.StringComparison.OrdinalIgnoreCase));
 var dtos = all.Select(c => _mapper.Map<ClienteDto>(c)).ToList();
 var paged = new StaticPagedList<ClienteDto>(dtos.Skip((page-1)*PageSize).Take(PageSize).ToList(), page, PageSize, dtos.Count);
 ViewData["Search"] = search;
 return View(paged);
 }

 [Authorize]
 public IActionResult Create() => View(new ClienteDto { FechaRegistro = System.DateTime.UtcNow, Activo = true });

 [HttpPost]
 [ValidateAntiForgeryToken]
 [Authorize]
 public async Task<IActionResult> Create(ClienteDto dto)
 {
 if(!ModelState.IsValid) return View(dto);
 var entity = _mapper.Map<Streaminds.Domain.Entities.Cliente>(dto);
 await _uow.Clientes.AddAsync(entity);
 await _uow.CommitAsync();
 return RedirectToAction(nameof(Index));
 }

 [Authorize]
 public async Task<IActionResult> Edit(int id)
 {
 var cl = await _uow.Clientes.GetByIdAsync(id);
 if(cl==null) return NotFound();
 return View(_mapper.Map<ClienteDto>(cl));
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 [Authorize]
 public async Task<IActionResult> Edit(int id, ClienteDto dto)
 {
 if(id!=dto.Id) return BadRequest();
 if(!ModelState.IsValid) return View(dto);
 var cl = await _uow.Clientes.GetByIdAsync(id);
 if(cl==null) return NotFound();
 _mapper.Map(dto, cl);
 await _uow.Clientes.Update(cl);
 await _uow.CommitAsync();
 return RedirectToAction(nameof(Index));
 }
 }
}
