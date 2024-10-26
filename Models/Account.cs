using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStoreManagementSystem.Models
{
    
    public class Account 
    {
        [Key]
        [DisplayName("รหัสผู้ใช้งาน")]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [DisplayName("ชื่อ")]
        public string FirstName {  get; set; }

        [DisplayName("นามสกุล")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [EmailAddress(ErrorMessage = "กรุณากรอกอีเมลให้ถูกต้อง")]
        [DisplayName("อีเมล")]
        public string Email { get; set; }

        [DisplayName("เบอร์โทรศัพท์")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง")]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อผู้ใช้งาน")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "ชื่อผู้ใช้งานต้องมีความยาวระหว่าง 4 ถึง 100 ตัวอักษร")]
        [DisplayName("ชื่อผู้ใช้งาน")]
        public string Username { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนรหัสผ่าน")]
        [MinLength(4, ErrorMessage = "รหัสผ่านต้องมีความยาวอย่างน้อย 4 ตัวอักษร")]
        [DisplayName("รหัสผ่าน")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "กรุณาป้อนรหัสผ่านอีกครั้งเพื่อยืนยัน")]
        [DisplayName("ยืนยันรหัสผ่าน")]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน กรุณาตรวจสอบ")]
        public string ConfirmPassword { get; set; }

        [DisplayName("ที่อยู่")]
        public string Address { get; set; }

        [DisplayName("สถานะ")]
        public string Status { get; set; }


        public Account()
        {
            // ตั้งค่าเริ่มต้นให้เป็น Customer
            Status = "Customer";
        }
    }
}
