using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Streaminds.Web.Models.Dto;
using System.Threading.Tasks;

namespace Streaminds.Web.Controllers
{
 public class AccountController : Controller
 {
 private readonly SignInManager<IdentityUser> _signInManager;
 private readonly UserManager<IdentityUser> _userManager;

 public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
 {
 _signInManager = signInManager;
 _userManager = userManager;
 }

 public IActionResult Login(string? returnUrl = null)
 {
 ViewData["ReturnUrl"] = returnUrl;
 return View(new LoginDto());
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Login(LoginDto dto, string? returnUrl = null)
 {
 if (!ModelState.IsValid) return View(dto);
 var res = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, lockoutOnFailure: false);
 if (res.Succeeded)
 {
 if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
 return RedirectToAction("Index", "Home");
 }
 ModelState.AddModelError(string.Empty, "Login inválido");
 return View(dto);
 }

 public IActionResult Register() => View(new RegisterDto());

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Register(RegisterDto dto)
 {
 if(!ModelState.IsValid) return View(dto);
 var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
 var result = await _userManager.CreateAsync(user, dto.Password);
 if (result.Succeeded)
 {
 await _signInManager.SignInAsync(user, isPersistent: false);
 TempData["Message"] = "Registro correcto. Bienvenido!";
 return RedirectToAction("Login", "Account");
 }
 foreach(var err in result.Errors) ModelState.AddModelError(string.Empty, err.Description);
 return View(dto);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Logout()
 {
 await _signInManager.SignOutAsync();
 return RedirectToAction("Index", "Home");
 }
 }
}
