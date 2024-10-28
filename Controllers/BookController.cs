using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineBookStoreManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _db;

        // Constructor หลัก
        public BookController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Category(int page = 1, int pageSize = 20)
        {
            IEnumerable<Category> categories = _db.Categories;
            var totalCategory = categories.Count();
            var totalPages = (int)Math.Ceiling(totalCategory / (double)pageSize);
            var cateriesOnPage = categories.Skip((page - 1) * pageSize).Take(pageSize);
            
            var currentIndexItem = (page - 1) * pageSize;
      

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentIndexItem = currentIndexItem;
            return View(cateriesOnPage);
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
