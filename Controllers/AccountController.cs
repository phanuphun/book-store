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
                    return RedirectToAction("Index","Book");
                }
                else
                {
                    TempData["LoginError"] = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["LoginError"] = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง";
                return RedirectToAction("Login");
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
                TempData["RegisterError"] = "ชื่อผู้ใช้งานหรืออีเมลที่สมัครมีการใช้งานแล้ว";
                return RedirectToAction("Register");
            }

            // ถ้า Phone ว่าง ให้ลบ error ที่อยู่ใน ModelState
            if (string.IsNullOrWhiteSpace(data.Phone))
            {
                ModelState.Remove("Phone");
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
                TempData["RegisterSuccess"] = "สมัครสมาชิกเรียบร้อย";
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

        public IActionResult UpdateMyAccount()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMyAccount(AccountUpdate data)
        {
          
            if (string.IsNullOrWhiteSpace(data.Phone))
            {
                ModelState.Remove("Phone");
                data.Phone = "";
            }

            if (string.IsNullOrWhiteSpace(data.LastName))
            {
                ModelState.Remove("LastName");
                data.LastName = "";
            }

            if (string.IsNullOrWhiteSpace(data.Address))
            {
                ModelState.Remove("Address");
                data.Address = "";  
            }

            if (ModelState.IsValid)
            {
                var user = _db.Accounts.Find(data.Id);
                if (user != null)
                {
                    user.FirstName = data.FirstName;
                    user.LastName = data.LastName;
                    user.Email = data.Email;
                    user.Username = data.Username;

                    user.Phone = data.Phone;
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
            return View(data);
        }

        public IActionResult Users(int page = 1, int pageSize = 10)
        {
            IEnumerable<Account> users = _db.Accounts;

            // get all totaluser
            var totalUsers = users.Count();
            // calculate page by totalUsers / pagesize e.g. 5/2 = 2.5 pages 
            // when result is an odd numbers use math and ceiling up e.g. 5/2 = 3 pages
            // use (int) pase from double to int
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);


            // get user on page
            // use .skip() for skip data by (page-1)*pagesize 
            // and use .take() for limit data
            // e.g. page 1 : (1-1)*2 = 0 data // no need to skip any data
            // e.g. page 2 : (2-1)*2 = 2 data // now we need to skip 2 data and get next 2 data
            // e.g. page 3 : (3-1)*2 = 4 data // skip 4 data and get 2 next data
            var usersOnPage = users.Skip((page - 1) * pageSize).Take(pageSize);
            var currentIndexItem = (page - 1) * pageSize;

            // use ViewBag for send obj from controller to View
            ViewBag.TotalPages = totalPages; 
            ViewBag.CurrentPage = page;
            ViewBag.CurrentIndexItem = currentIndexItem;
            return View(usersOnPage);
        }

        public IActionResult DeleteAccount(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var user = _db.Accounts.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.Accounts.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Users");
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
