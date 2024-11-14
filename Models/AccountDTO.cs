using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineBookStoreManagementSystem.Models
{
    public class AccountDTO
    {
        [Key]
        [DisplayName("รหัสผู้ใช้งาน")]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อ")]
        [DisplayName("ชื่อ")]
        public string FirstName { get; set; }

        [DisplayName("นามสกุล")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนอีเมล")]
        [EmailAddress(ErrorMessage = "กรุณากรอกอีเมลให้ถูกต้อง")]
        [DisplayName("อีเมล")]
        public string Email { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อผู้ใช้งาน")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "ชื่อผู้ใช้งานต้องมีความยาวระหว่าง 4 ถึง 100 ตัวอักษร")]
        [DisplayName("ชื่อผู้ใช้งาน")]
        public string Username { get; set; }

        [DisplayName("ที่อยู่")]
        public string Address { get; set; } // ค่านี้จะเป็นค่าว่างได้

        [DisplayName("เบอร์โทรศัพท์")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "เบอร์โทรศัพท์ต้องเป็นตัวเลข 10 หลัก")]
        public string Phone { get; set; } // ค่านี้จะเป็นค่าว่างได้

    }
}
