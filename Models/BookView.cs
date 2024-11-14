namespace OnlineBookStoreManagementSystem.Models
{
    public class BookView
    {
        public BookDTO? Book { get; set; }   
        public IEnumerable<Category>? Categories { get; set; }   
    }

    public class BooksViewHome
    {
        public IEnumerable<Book>? Books { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

    }
}
