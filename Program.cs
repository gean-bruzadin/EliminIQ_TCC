using EliminIQ_TCC.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra MVC e DbContext
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbConfig>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Registra cache em memória e sessão
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 3. Middlewares
if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

// **Antes de Autorization**:
app.UseSession();

app.UseAuthorization();

// 4. Rota padrão: agora Jogador/Login como inicial
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Jogador}/{action=Login}/{id?}"
);

app.Run();
