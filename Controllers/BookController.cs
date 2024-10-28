using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineBookStoreManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment environment;

        // Constructor หลัก
        public BookController(AppDbContext db , IWebHostEnvironment environment)
        {
            _db = db;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookStock(int page = 1, int pageSize = 20)
        {
            IEnumerable<Book> books = _db.Books;
            var totalBook = books.Count();
            var totalPages = (int)Math.Ceiling(totalBook / (double)pageSize);
            var bookOnPage = books.Skip((page - 1) * pageSize).Take(pageSize);

            var currentIndexItem = (page - 1) * pageSize;

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentIndexItem = currentIndexItem;
            return View(bookOnPage);
        }

        public IActionResult AddBook()
        {
            var categories = _db.Categories.ToList();
            var bookView = new BookView
            {
                Book = new BookDTO(),  // หรือกำหนดค่าเริ่มต้นถ้ามี
                Categories = categories
            };

            return View(bookView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddBook(BookView bookObj)
        {
            var bookDTO = bookObj.Book;
            // remove fields 
            var fieldsToRemove = new List<string> {
                "Book.Description",
                "Book.Drawer",
                "Book.Translater",
                "Book.Pages",
                "Book.Thickness",
                "Book.Weight",
                "Book.Size",
                "Book.Categories",
                "Book.Category",
                "Categories"
            };  

            foreach (var field in fieldsToRemove)
            {
                ModelState.Remove(field); 
            }

            //check image 
            if (bookObj.Book.Image == null)
            {
                TempData["AddBookErr"] = "image null";
                ModelState.AddModelError("Image","กรุณาเพิ่มภาพหน้าปกหนังสือ");
            }

            //form validate
            if (!ModelState.IsValid)
            {
                TempData["AddBookErr"] = "กรุณากรอกข้อมูลที่จำเป็นให้ครบ";
                bookObj.Categories = _db.Categories.ToList();
                return View(bookObj);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(bookDTO.Image!.FileName);

            string imageFullPath = environment.WebRootPath + "/bookCovers/" + newFileName;
            using (var straem = System.IO.File.Create(imageFullPath))
            {
                bookDTO.Image.CopyTo(straem);
            }

            
            Book book = new Book()
            {
                Title = bookDTO.Title,
                Image = newFileName,
                CategoryId = bookDTO.CategoryId,
                Author = bookDTO.Author,
                Price = bookDTO.Price,
                Amount = bookDTO.Amount,

                Description = bookDTO.Description ?? "",
                Translater = bookDTO.Translater ?? "", 
                Drawer = bookDTO.Drawer ?? "",
                Pages = bookDTO.Pages ?? "",
                Weight = bookDTO.Weight ?? "",
                Thickness = bookDTO.Thickness ?? "",
                Size = bookDTO.Size ?? ""
            };

            _db.Books.Add(book);
            _db.SaveChanges();

            TempData["AddBookSucc"] = "เพิ่มหนังสือเรียบร้อย";
            return RedirectToAction("AddBook");
        }

        public IActionResult Category(int page = 1, int pageSize = 20)
        {
            IEnumerable<Category> categories = _db.Categories;
            var totalCategory = categories.Count();
            var totalPages = (int)Math.Ceiling(totalCategory / (double)pageSize);
            var categoriesOnPage = categories.Skip((page - 1) * pageSize).Take(pageSize);
            
            var currentIndexItem = (page - 1) * pageSize;
      

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentIndexItem = currentIndexItem;
            return View(categoriesOnPage);
        }

        public IActionResult AddCategory()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(string name)
        {
            var exitingName = _db.Categories.FirstOrDefault(
            u => u.Name == name);

            if (exitingName != null)
            {
                TempData["AddCateErr"] = "มีชื่อหมวดหมู่นี้แล้ว " + name;
                return RedirectToAction("AddCategory");
            }

            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = name
                };
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["AddCateSucc"] = "เพิ่มหมวดหมู่สำเร็จ";
                return RedirectToAction("AddCategory");
            }
            else
            {
                TempData["AddCateErr"] = "กรุณากรอกชื่อหมวดหมู่";
                return RedirectToAction("AddCategory");
            }
            return View(name);
        }

        public IActionResult UpdateCategory(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _db.Categories.Find(id);

            if (categoryObj == null)
            {
                return NotFound();
            }

            return View(categoryObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category data)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(data);
                _db.SaveChanges();
                TempData["UpdateCateSucc"] = "บนทึกข้อมูลเรียบร้อย";
                return RedirectToAction("UpdateCategory");
            }
            return View(data);
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            TempData["DelSucc"] = "ลบหมวดหมู่สำเร็จ";

            return RedirectToAction("Category");
        }
    }
}
