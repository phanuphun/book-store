using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStoreManagementSystem.Models
{
    public class Book
    {
        [Key , DisplayName("รหัสหนังสือ")]
        public int id { get; set; }

        [Required(ErrorMessage ="กรุณาเพิ่มภาพหน้าปกหนังสือ"),DisplayName("ภาพหน้าปกหนังสือ")]
        public string image { get; set; }

        [Required(ErrorMessage ="กรุป้อนชื่อหนังสือ"),DisplayName("ชื่อหนังสือ")]
        public string title { get; set; }

        [DisplayName("รายละเอียด")]
        public string description { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อผู้เขียน"), DisplayName("ผู้เขียน")]
        public string author { get; set; }

        [DisplayName("นักวาด")]
        public string drawer { get; set; }

        [DisplayName("ผู้แปล")]
        public string translater { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนราคา"), DisplayName("ราคา")]
        public double price { get; set; }

        [DisplayName("จำนวนหน้า")]
        public int pages { get; set; }

        [DisplayName("ความหนา/สูง")]
        public string thickness { get; set;}

        [Required(ErrorMessage = "กรุณาป้อนจำนวนหนังสือในสตอก"), DisplayName("จำนวนหนังสือ")]
        public int amount {  get; set; }

        [DisplayName("ขนาดของหนังสือ(กxย)")]
        public string size { get; set; }

        [DisplayName("หมวดหมู่")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
