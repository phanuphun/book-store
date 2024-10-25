using Microsoft.AspNetCore.Mvc;

namespace OnlineBookStoreManagementSystem.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = username; // ส่งค่าไปยัง ViewData
            return View();
        }
    }
}
