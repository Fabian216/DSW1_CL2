using ProyDSW1_T2_ALAN_OLAYA_EDGAR_FABIAN.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1
builder.Services.AddScoped<ArticulosDAO>();

// 2 Establecer el tiempo de duracion de las variables de session
builder.Services.AddSession(s =>
                s.IdleTimeout = TimeSpan.FromMinutes(20));

var app = builder.Build();

// 3 Habilitar las variables de session
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
