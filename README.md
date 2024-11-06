
         
      
    
## Book Store Management System
ระบบแบ่งออกเป็น 2 ส่วน ได้แก่ผู้ใช้งานทั่วไป และผู้ดูแลระบบ <br>
- [x] สมัครสมาชิก / เข้าสู่ระบบ
- [x] เพิ่ม ลบ แก้ไข ผู้ใช้งาน / แก้ไขข้อมูลส่วนตัว
- [x] เพิ่ม ลบ แก้ไข หมวดหมู่หนังสือ
- [x] เพิ่ม ลบ แก้ไข หนังสือ
- [ ] ระบบตะกร้าสินค้า
- [ ] จัดการรายการสั่งซื้อ
- [ ] ประวัติรายกายการสั่งซื้อ

## Installation
1. `dotnet --version` : ตรวจสอบว่าลง .NET CORE SDK หรือยัง หากยังไม่ลงให้ติดตั้งก่อนเป็นอันดับแรก 
2. `dotnet restore` : ติดตั้ง Package ที่อยู่ใน .csproj (Entity Framework Core (EF Core))
3. ตั้งค่าการเชื่อมต่อฐานข้อมูลกับ SQL Server ที่ appsettings.json จากนั้นให้เปลี่ยนชื่อ Server Name ของเราให้ตรงกับ SQL Server
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=<ชื่อฐานข้อมูล>\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;TrustServerCertificate=True;"
  }
```
4. `dotnet ef database update` : Migrate ฐานข้อมูลขึ้น SQL Server  
5. `dotnet run` รันโปรเจ็ค