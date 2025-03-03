using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVCv1.Models;
using MVCv1.Repositoria;

var builder = WebApplication.CreateBuilder(args);
// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// ��������� �������� BlogContext � �������� ������� � ����������
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
// ����������� ������� ����������� ��� �������������� � ����� ������
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
// 3.��������� ��������� ����������� ����� Middleware 

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
