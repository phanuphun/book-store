using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineBookStoreManagementSystem.Models
{
    public class BookDTO
    {
        [Key, DisplayName("รหัสหนังสือ")]
        public int Id { get; set; } 

        [Required(ErrorMessage = "กรุณาเพิ่มภาพหน้าปกหนังสือ"), DisplayName("ภาพหน้าปกหนังสือ")]
        public IFormFile Image { get; set; }   

        [Required(ErrorMessage = "กรุณาป้อนชื่อหนังสือ"), DisplayName("ชื่อหนังสือ")]
        public string Title { get; set; }  

        [DisplayName("รายละเอียด")]
        public string? Description { get; set; }  

        [Required(ErrorMessage = "กรุณาป้อนชื่อผู้เขียน"), DisplayName("ผู้เขียน")]
        public string? Author { get; set; } 
        [DisplayName("นักวาด")]
        public string? Drawer { get; set; }  

        [DisplayName("ผู้แปล")] 
        public string? Translater { get; set; } 

        [Required(ErrorMessage = "กรุณาป้อนราคา"), DisplayName("ราคา")]
        [Range(1, double.MaxValue, ErrorMessage = "กรุณาป้อนราคาให้ถูกต้อง หรือมากกว่า 0")]
        public double Price { get; set; }  

        [DisplayName("จำนวนหน้า")]
        public string? Pages { get; set; } 

        [DisplayName("ความหนา/สูง")]
        public string? Thickness { get; set; }  

        [DisplayName("น้ำหนัก")]
        public string? Weight { get; set; }  

        [Required(ErrorMessage = "กรุณาป้อนจำนวนหนังสือในสตอก"), DisplayName("จำนวนหนังสือ")]
        public int Amount { get; set; } 

        [DisplayName("ขนาดของหนังสือ(กxย)")]
        public string? Size { get; set; }  

        [Required(ErrorMessage = "กรุณาเลือกหมวดหมู่"), DisplayName("หมวดหมู่")]
        public int CategoryId { get; set; } 

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
