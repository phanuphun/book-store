using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;
using System.Text;
using System.Security.Cryptography;
 


namespace OnlineBookStoreManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // สร้าง Instant สำหรับ Db ขึ้นมาโดยใช้ DI
        private readonly AppDbContext _db;

        // parameter db คือ Instant ที่สร้างมาจาก DI Container
        public AccountController(AppDbContext db )
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = username; // ส่งค่าไปยัง ViewData

            return View();
        }

        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account data)
        {
            var user = _db.Accounts.FirstOrDefault(u => u.Username == data.Username);
            if(user != null)
            {
                if(VerifyPassword(data.Password,user.Password))
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    TempData["LoginSuccess"] = "เข้าสู่ระบบสำเร็จ";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["LoginError"] = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง";
                    return RedirectToAction("Login");
                }
            }
            return View(data);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Account data)
        {
            var exitingUser = _db.Accounts.FirstOrDefault(
                u => u.Username == data.Username || u.Email == data.Email
            );

            if (exitingUser != null)
            {
                TempData["RegisterError"] = "ชื่อผู้ใช้งานหรืออีเมลนี้มีอยู่ในระบบแล้ว! กรุณาตรวจสอบใหม่อีครั้ง";
                return RedirectToAction("Register");
            }

            if (ModelState.IsValid)
            {
                string hashPassword = HashPassword(data.Password);

                var user = new Account
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = hashPassword
                };

                _db.Accounts.Add(user);
                _db.SaveChanges();
                TempData["RegisterSuccess"] = "สมัครสมาชิกสำเร็จ! กรุณาล็อกอินเพื่อเข้าสู่ระบบ";
                return RedirectToAction("Register");
            }
            return View(data);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }

 
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfInput = HashPassword(enteredPassword);
            return hashOfInput == storedHash;
        }
    }
}
