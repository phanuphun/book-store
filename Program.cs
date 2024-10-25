using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
 * 1. builder : เป็นตัวแปรที่เก็บค่า WebApplicationBuilder ซึ่งถูกสร้างขึ้นมาเมื่อคุณเรียกใช้ WebApplication.CreateBuilder(args)
 * 2. Services :  เป็น Collection ของบริการ (service collection) ที่ใช้ในการจัดการ Dependency Injection (DI) 
 * ใน ASP.NET Core. บริการที่คุณลงทะเบียนที่นี่จะถูกใช้ในส่วนต่าง ๆ ของแอปพลิเคชัน
 * 3. AddDbContext<TContext>() : คือการเรียกใช้วิธีการที่ใช้สำหรับลงทะเบียน DbContext ใน DI container ของ ASP.NET Core
 * 4. <AppDbContext> : AppDbContext คือชื่อของคลาสที่คุณสร้างขึ้นเพื่อสืบทอดมาจาก DbContext. การลงทะเบียน DbContext 
 * ทำให้สามารถสร้างอินสแตนซ์ของมันผ่าน DIได้ทุกที่ในแอปพลิเคชันของคุณ โดยไม่ต้องสร้างใหม่ด้วยตนเอง.
 * 5. options => : เป็น lambda expression  หมายถึงว่าเมื่อมีการสร้าง AppDbContext จะใช้การตั้งค่าที่ระบุในฟังก์ชันนี้
 * 6. options.UseSqlServer(...) :  เป็นวิธีการที่ใช้ในการกำหนดให้ DbContext ใช้ SQL Server เป็นฐานข้อมูล. 
 * ฟังก์ชันนี้จะรับค่าพารามิเตอร์ที่เป็น DbContextOptionsBuilder ซึ่งคุณจะสามารถตั้งค่าต่าง ๆ ได้ที่นี่
 * 7. builder.Configuration : เข้าถึง configuration ของแอปพลิเคชัน ซึ่งถูกกำหนดไว้ในไฟล์ appsettings.json
 * 8. GetConnectionString("DefaultConnection") : เป็นการเรียกใช้วิธีการเพื่อดึงค่าของการเชื่อมต่อ (connection string) 
 * ที่ชื่อว่า "DefaultConnection" จาก configuration การเชื่อมต่อนี้จะถูกใช้ในการเชื่อมต่อไปยังฐานข้อมูล SQL Server.
 */
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// เพิ่มการสนับสนุนสำหรับเซสชัน
builder.Services.AddDistributedMemoryCache(); // ใช้ Memory Cache สำหรับเซสชัน
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // เวลาหมดอายุของเซสชัน
    options.Cookie.HttpOnly = true; // ให้คุกกี้ไม่สามารถเข้าถึงจาก JavaScript
    options.Cookie.IsEssential = true; // ต้องการตั้งค่าเป็น essential cookie
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

app.UseSession(); // ต้องอยู่ก่อน app.UseRouting()

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}"); 

app.Run();
