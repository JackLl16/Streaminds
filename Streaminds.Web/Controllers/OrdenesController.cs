using Microsoft.AspNetCore.Mvc;
using Streaminds.Domain.Contracts;
using Streaminds.Domain.Entities;
using Streaminds.Web.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Streaminds.Web.Controllers
{
 [Authorize]
 public class OrdenesController : Controller
 {
 private readonly IUnitOfWork _uow;
 public OrdenesController(IUnitOfWork uow)
 {
 _uow = uow;
 }

 public async Task<IActionResult> Create()
 {
 var clientes = await _uow.Clientes.GetAll();
 var productos = await _uow.ProductosStreaming.GetAll();
 ViewData["Clientes"] = clientes;
 ViewData["Productos"] = productos;
 return View(new OrdenCreateDto());
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(OrdenCreateDto dto)
 {
 if (!ModelState.IsValid)
 {
 var clientes = await _uow.Clientes.GetAll();
 var productos = await _uow.ProductosStreaming.GetAll();
 ViewData["Clientes"] = clientes;
 ViewData["Productos"] = productos;
 return View(dto);
 }

 // Crear orden
 var orden = new Orden
 {
 Fecha = DateTime.UtcNow,
 ClienteId = dto.ClienteId,
 Estado = "Pendiente",
 Total =0m
 };

 // Agregar detalles y accesos
 foreach (var item in dto.Items)
 {
 var producto = await _uow.ProductosStreaming.GetByIdAsync(item.ProductoStreamingId);
 if (producto == null) continue; // o manejar error
 var detalle = new DetalleOrden
 {
 ProductoStreamingId = producto.Id,
 Cantidad = item.Cantidad,
 PrecioUnitario = producto.Precio,
 Subtotal = producto.Precio * item.Cantidad
 };
 orden.DetalleOrdenes.Add(detalle);

 var acceso = new AccesoUsuario
 {
 ClienteId = dto.ClienteId,
 ProductoStreamingId = producto.Id,
 FechaActivacion = DateTime.UtcNow,
 FechaExpiracion = DateTime.UtcNow.AddDays(producto.DuracionDias)
 };
 await _uow.AccesosUsuario.AddAsync(acceso);
 orden.Total += detalle.Subtotal;
 }

 await _uow.Ordenes.AddAsync(orden);
 await _uow.CommitAsync();
 return RedirectToAction("Index", "Home");
 }
 }
}
