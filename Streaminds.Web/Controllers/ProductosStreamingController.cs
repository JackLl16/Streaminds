using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Streaminds.Domain.Contracts;
using Streaminds.Domain.Entities;
using Streaminds.Web.Models.Dto;
using X.PagedList;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Streaminds.Web.Controllers
{
 public class ProductosStreamingController : Controller
 {
 private readonly IUnitOfWork _unitOfWork;
 private readonly IMapper _mapper;
 private const int PageSize =10;

 public ProductosStreamingController(IUnitOfWork unitOfWork, IMapper mapper)
 {
 _unitOfWork = unitOfWork;
 _mapper = mapper;
 }

 [AllowAnonymous]
 public async Task<IActionResult> Index(string? search, int page =1)
 {
 var all = await _unitOfWork.ProductosStreaming.GetAll();
 // If the user is not authenticated (client view), show only active products
 var isAuthenticated = User?.Identity?.IsAuthenticated == true;
 if (!isAuthenticated)
 {
 all = all.Where(p => p.Activo);
 }

 if (!string.IsNullOrWhiteSpace(search))
 {
 all = all.Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase));
 }
 var dtos = all.Select(p => _mapper.Map<ProductoStreamingDto>(p)).ToList();
 var total = dtos.Count;
 var subset = dtos.Skip((page -1) * PageSize).Take(PageSize).ToList();
 var paged = new StaticPagedList<ProductoStreamingDto>(subset, page, PageSize, total);
 ViewData["Search"] = search;
 return View(paged);
 }

 [Authorize]
 public IActionResult Create()
 {
 return View(new ProductoStreamingDto());
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 [Authorize]
 public async Task<IActionResult> Create(ProductoStreamingDto dto)
 {
 if (!ModelState.IsValid) return View(dto);
 var entity = _mapper.Map<ProductoStreaming>(dto);
 await _unitOfWork.ProductosStreaming.AddAsync(entity);
 await _unitOfWork.CommitAsync();
 return RedirectToAction(nameof(Index));
 }

 [Authorize]
 public async Task<IActionResult> Edit(int id)
 {
 var entity = await _unitOfWork.ProductosStreaming.GetByIdAsync(id);
 if (entity == null) return NotFound();
 var dto = _mapper.Map<ProductoStreamingDto>(entity);
 return View(dto);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 [Authorize]
 public async Task<IActionResult> Edit(int id, ProductoStreamingDto dto)
 {
 if (id != dto.Id) return BadRequest();
 if (!ModelState.IsValid) return View(dto);
 var entity = await _unitOfWork.ProductosStreaming.GetByIdAsync(id);
 if (entity == null) return NotFound();
 _mapper.Map(dto, entity);
 await _unitOfWork.ProductosStreaming.Update(entity);
 await _unitOfWork.CommitAsync();
 return RedirectToAction(nameof(Index));
 }

 [Authorize]
 public async Task<IActionResult> Delete(int id)
 {
 var entity = await _unitOfWork.ProductosStreaming.GetByIdAsync(id);
 if (entity == null) return NotFound();
 var dto = _mapper.Map<ProductoStreamingDto>(entity);
 return View(dto);
 }

 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 [Authorize]
 public async Task<IActionResult> DeleteConfirmed(int id)
 {
 var entity = await _unitOfWork.ProductosStreaming.GetByIdAsync(id);
 if (entity == null) return NotFound();
 await _unitOfWork.ProductosStreaming.Remove(entity);
 await _unitOfWork.CommitAsync();
 return RedirectToAction(nameof(Index));
 }
 }
}
