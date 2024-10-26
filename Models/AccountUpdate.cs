using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineBookStoreManagementSystem.Models
{
    public class AccountUpdate
    {
        [Key]
        [DisplayName("รหัสผู้ใช้งาน")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ชื่อเป็นค่าว่างไม่ได้")]
        [DisplayName("ชื่อ")]
        public string FirstName { get; set; }

        [DisplayName("นามสกุล")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "อีเมลเป็นค่าว่างไม่ได้")]
        [EmailAddress(ErrorMessage = "กรุณากรอกอีเมลให้ถูกต้อง")]
        [DisplayName("อีเมล")]
        public string Email { get; set; }

        [DisplayName("เบอร์โทรศัพท์")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "ชื่อผู้ใช้งานจำเป็นต้องป้อนเพื่อใช้ในการเข้าสู่ระบบ")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "ชื่อผู้ใช้งานต้องมีความยาวระหว่าง 4 ถึง 100 ตัวอักษร")]
        [DisplayName("ชื่อผู้ใช้งาน")]
        public string Username { get; set; }

        [DisplayName("ที่อยู่")]
        public string Address { get; set; }
 
    }
}
