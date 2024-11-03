
         
      
    
## Book Store Management System
  
ระบบจัดการร้านหนังสือ ทำขึ้นมาเพื่อศึกษาการพัฒนาเว็บด้วย ASP.NET CORE MVC ระบบแบ่งออกเป็น 2 ส่วน ได้แก่ผู้ใช้งานทั่วไป และผู้ดูแลระบบ <br>

**ฟังก์ชันการใช้งาน**
- [x] สมัครสมาชิก / เข้าสู่ระบบ
- [x] เพิ่ม ลบ แก้ไข ผู้ใช้งาน / แก้ไขข้อมูลส่วนตัว
- [x] เพิ่ม ลบ แก้ไข หมวดหมู่หนังสือ
- [ ] เพิ่ม ลบ แก้ไข หนังสือ
- [ ] ระบบตะกร้าสินค้า
- [ ] จัดการรายการสั่งซื้อ
- [ ] ประวัติรายกายการสั่งซื้อ

## Installation
1. ตรวจสอบว่าลง `.NET CORE SDK` หรือยัง หากยังไม่ลงให้ติดตั้งก่อนเป็นอันดับแรก
```csharp
dotnet --version
```
2. ติดตั้ง Package ที่อยู่ใน `.csproj` เพื่อใช้งาน **Entity Framework Core (EF Core)**  
```shell
dotnet restore
```
3. ตั้งค่าการเชื่อมต่อฐานข้อมูลกับ SQL_Sever ที่ appsettings.json จากนั้นให้เปลี่ยนชื่อ Server Name ของเราให้ตรงกับ SQL Server `ConnectionStrings` 
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=<ชื่อฐานข้อมูล>\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;TrustServerCertificate=True;"
  }
```
4. Migrate ฐานข้อมูลขึ้น SQL Server ด้วย EF Core 
```shell
dotnet ef database update
```
5. รันโปรเจ็คบน localhost
```shell
dotnet run
```


## Tech Stack
<div align="left">
	<code><img width="80" src="https://user-images.githubusercontent.com/25181517/121405754-b4f48f80-c95d-11eb-8893-fc325bde617f.png" alt=".NET Core" title=".NET Core"/></code>
	<code><img width="80" src="https://user-images.githubusercontent.com/25181517/121405384-444d7300-c95d-11eb-959f-913020d3bf90.png" alt="C#" title="C#"/></code>
	<code><img width="80" src="https://github.com/marwin1991/profile-technology-icons/assets/19180175/3b371807-db7c-45b4-8720-c0cfc901680a" alt="MSSQL" title="MSSQL"/></code>
	<code><img width="80" src="https://user-images.githubusercontent.com/25181517/183898054-b3d693d4-dafb-4808-a509-bab54cf5de34.png" alt="Bootstrap" title="Bootstrap"/></code>
</div>
