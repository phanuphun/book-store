using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookStoreManagementSystem.Models
{
    public class Category
    {
        [Key]
        [DisplayName("รหัสหมวดหมู่")]
        public int Id { get; set; }

        [Required(ErrorMessage ="ชื่อหมวดหมู่หนังสือ"),DisplayName("ชื่อหมวดหมู่")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
