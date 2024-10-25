using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
 * 1. builder : �繵���÷���纤�� WebApplicationBuilder ��觶١���ҧ���������ͤس���¡�� WebApplication.CreateBuilder(args)
 * 2. Services :  �� Collection �ͧ��ԡ�� (service collection) �����㹡�èѴ��� Dependency Injection (DI) 
 * � ASP.NET Core. ��ԡ�÷��سŧ����¹�����ж١�����ǹ��ҧ � �ͧ�ͻ���पѹ
 * 3. AddDbContext<TContext>() : ��͡�����¡���Ըա�÷��������Ѻŧ����¹ DbContext � DI container �ͧ ASP.NET Core
 * 4. <AppDbContext> : AppDbContext ��ͪ��ͧ͢���ʷ��س���ҧ��������׺�ʹ�Ҩҡ DbContext. ���ŧ����¹ DbContext 
 * ���������ö���ҧ�Թ�ᵹ��ͧ�ѹ��ҹ DI��ء�����ͻ���पѹ�ͧ�س ������ͧ���ҧ������µ��ͧ.
 * 5. options => : �� lambda expression  ���¶֧���������ա�����ҧ AppDbContext �����õ�駤�ҷ���к�㹿ѧ��ѹ���
 * 6. options.UseSqlServer(...) :  ���Ըա�÷����㹡�á�˹���� DbContext �� SQL Server �繰ҹ������. 
 * �ѧ��ѹ�����Ѻ��Ҿ������������� DbContextOptionsBuilder ��觤س������ö��駤�ҵ�ҧ � ������
 * 7. builder.Configuration : ��Ҷ֧ configuration �ͧ�ͻ���पѹ ��觶١��˹�������� appsettings.json
 * 8. GetConnectionString("DefaultConnection") : �繡�����¡���Ըա�����ʹ֧��Ңͧ����������� (connection string) 
 * ��������� "DefaultConnection" �ҡ configuration ����������͹��ж١��㹡������������ѧ�ҹ������ SQL Server.
 */
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// �������ʹѺʹع����Ѻ�ʪѹ
builder.Services.AddDistributedMemoryCache(); // �� Memory Cache ����Ѻ�ʪѹ
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ����������آͧ�ʪѹ
    options.Cookie.HttpOnly = true; // ���ء����������ö��Ҷ֧�ҡ JavaScript
    options.Cookie.IsEssential = true; // ��ͧ��õ�駤���� essential cookie
});

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

app.UseSession(); // ��ͧ�����͹ app.UseRouting()

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}"); 

app.Run();
