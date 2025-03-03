using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVCv1.Models;
using MVCv1.Repositoria;

var builder = WebApplication.CreateBuilder(args);
// получаем строку подключени€ из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// добавл€ем контекст BlogContext в качестве сервиса в приложение
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
// регистраци€ сервиса репозитори€ дл€ взаимодействи€ с базой данных
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// 3.ƒобавл€ем компонент логировани€ через Middleware 

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Register}/{id?}");

app.Run();
