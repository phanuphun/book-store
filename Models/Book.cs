using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookStoreManagementSystem.Models
{
    public class Book
    {
        [Key]
        [DisplayName("รหัสหนังสือ")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อหนังสือ")]
        [DisplayName("ชื่อหนังสือ")]
        public string Title { get; set; }

        [DisplayName("ชื่อผู้เขียน")]
        [MaxLength(100,ErrorMessage ="ชื่อมีขนาดความยาวเกินไป ระบุที่ 100 ตัวอักษรเท่านั้น")]
        public string Author { get; set; }

        public int YearPublished { get; set; }   
        public DateTime Date { get; set; }
    }
}
