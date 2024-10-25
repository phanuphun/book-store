/* 
 * DbContext ใน Entity Framework Core (EF Core) คือคลาสที่ทำหน้าที่เป็นสะพานเชื่อมระหว่างแอปพลิเคชันกับฐานข้อมูล 
 * ซึ่งช่วยให้เราทำงานกับข้อมูลในฐานข้อมูลผ่าน C# objects (Models) แทนที่จะต้องเขียน SQL โดยตรง 
 * เช่น การสร้าง อ่าน แก้ไข และลบ (CRUD operations) ข้อมูล
 */
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreManagementSystem.Models;

namespace OnlineBookStoreManagementSystem.Data
{
    /*
     * กำหนดให้ AppDbContext สืบทอดจาก DbContext ซึ่งเป็นคลาสหลักของ EF Core
     * DbContext จะช่วยให้เราจัดการข้อมูลจากฐานข้อมูลด้วยวิธีการแบบ OOP 
     */
    public class AppDbContext:DbContext
    {
        /*
         * Constructor ของคลาส AppDbContext ใช้รับ DbContextOptions ซึ่งเป็นอ็อบเจ็กต์ที่กำหนดค่าเกี่ยวกับการเชื่อมต่อฐานข้อมูล 
         * คำสั่ง base(options) จะส่ง options ไปยังคลาสแม่ (DbContext) เพื่อให้ EF Core รู้ว่าควรเชื่อมกับฐานข้อมูลแบบใด
         */
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        /*
         * DbSet<T> ใช้แทน "ตาราง" ในฐานข้อมูล โดยที่ T คือตัวอย่างของโมเดล (Model Class) ที่เราใช้
         */
        public DbSet<Account> Accounts { get; set; }
    }
}
