using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Streaminds.Domain.Contracts;
using Streaminds.Infrastructure.Data; // DbContext and DbSeeder
using Streaminds.Infrastructure.Repositories; // UnitOfWork

var builder = WebApplication.CreateBuilder(args);

//1. Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
 ?? "Server=(localdb)\\mssqllocaldb;Database=StreamindsDb;Trusted_Connection=True;MultipleActiveResultSets=true";

//2. DbContext
builder.Services.AddDbContext<StreamindsDbContext>(options =>
 options.UseSqlServer(connectionString));

//3. Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
 options.SignIn.RequireConfirmedAccount = false;
})
 .AddEntityFrameworkStores<StreamindsDbContext>()
 .AddDefaultTokenProviders();

//4. MVC / Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//5. App services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Streaminds.Web.Mapping.MappingProfile));

var app = builder.Build();

// Apply migrations and seed
using (var scope = app.Services.CreateScope())
{
 var services = scope.ServiceProvider;
 try
 {
 var ctx = services.GetRequiredService<StreamindsDbContext>();
 await ctx.Database.MigrateAsync();
 await DbSeeder.SeedAsync(ctx);
 }
 catch (Exception ex)
 {
 var logger = services.GetRequiredService<ILogger<Program>>();
 logger.LogError(ex, "An error occurred while migrating or seeding the database.");
 }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
 app.UseExceptionHandler("/Home/Error");
 app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();