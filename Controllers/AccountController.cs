using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreManagementSystem.Data;
using OnlineBookStoreManagementSystem.Models;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

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
            //var username = HttpContext.Session.GetString("Username");
            //ViewData["Username"] = username;

            var idString = HttpContext.Session.GetString("Id");
            if (string.IsNullOrEmpty(idString) || !int.TryParse(idString, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var account = _db.Accounts.Find(id);
            if (account == null)
            {
                return NotFound(); // หรือทำการจัดการในแบบที่คุณต้องการ
            }
            var accountUpdate = new AccountUpdate
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Phone = account.Phone,
                Username = account.Username,
                Address = account.Address
            };

            return View(accountUpdate);
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
                    Debug.WriteLine("users data:" + user);
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    HttpContext.Session.SetString("Status", user.Status);
                    HttpContext.Session.SetString("Username", user.FirstName + " "+ user.LastName);
                    TempData["LoginSuccess"] = "เข้าสู่ระบบสำเร็จ";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["LoginError"] = "Username or password is incorrect.";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["LoginError"] = "Username or password is incorrect.";
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
                TempData["RegisterError"] = "This username or email already exists! Please check again.";
                return RedirectToAction("Register");
            }

            if (ModelState.IsValid)
            {
                string hashPassword = HashPassword(data.Password);

                var user = new Account
                {
                    FirstName = data.FirstName, 
                    LastName = data.LastName,
                    Username = data.Username,
                    Email = data.Email,
                    Phone = data.Phone,
                    Password = hashPassword,
                    Address = data.Address,
                };

                _db.Accounts.Add(user);
                _db.SaveChanges();
                TempData["RegisterSuccess"] = "Account registration completed!!";
                return RedirectToAction("Register");
            }
            return View(data);
        }
        public IActionResult Logout()
        {
            //HttpContext.Session.Remove("Username");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMyAccount(AccountUpdate data)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Accounts.Find(data.Id);
                if (user != null)
                {
                    user.FirstName = data.FirstName;
                    user.LastName = data.LastName;
                    user.Email = data.Email;
                    user.Phone = data.Phone;
                    user.Username = data.Username;
                    user.Address = data.Address;

                    _db.SaveChanges();
                    HttpContext.Session.SetString("Username", user.FirstName + " " + user.LastName);
                    TempData["msgSucc"] = "บันทึกข้อมูลเรียบร้อย!";
                }
                else
                {
                    TempData["msgErr"] = "ไม่พบผู้ใช้นี้";
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    TempData["msgErr"] += error.ErrorMessage + " ";  
                }
                
            }
            return RedirectToAction("Index");
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
