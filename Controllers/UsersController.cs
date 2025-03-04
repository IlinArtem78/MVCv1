using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCv1.Models;
using MVCv1.Repositoria;
using System.Diagnostics;

namespace MVCv1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IBlogRepository _repo;
        private readonly ILog _log;

        public UsersController(ILogger<UsersController> logger, IBlogRepository repo, ILog log)
        {
            _logger = logger;
            _log = log;
            _repo = repo;
           
        }
        /*
        public async Task<IActionResult> Index()
        {
            try
            {
                // Добавим создание нового пользователя
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Andrey",
                    LastName = "Petrov",
                    JoinDate = DateTime.Now
                };




                // Добавим в базу
                await _repo.AddUser(newUser);

                // Выведем результат
                Console.WriteLine($"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return View();

        }
        */



        public async Task<IActionResult> Authors()
        {

            try
            {
                var valueUrl = HttpContext.Items["url"] as string;
                if (HttpContext.Items.TryGetValue("CurrentDateTime", out var dateTimeObject))
                {
                    DateTime valueDate = (DateTime)dateTimeObject;
                    // Добавим создание нового лога
                    var newReques = new Request()
                    {
                        Id = Guid.NewGuid(),
                        Date = valueDate,
                        Url = valueUrl
                    };
                    await _log.Log(newReques);
                }
                var authors = await _repo.GetUsers();

                return View(authors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());


            }
            return View();


        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var valueUrl = HttpContext.Items["url"] as string;
            if (HttpContext.Items.TryGetValue("CurrentDateTime", out var dateTimeObject))
            {
                DateTime valueDate = (DateTime)dateTimeObject;
                // Добавим создание нового лога
                var newReques = new Request()
                {
                    Id = Guid.NewGuid(),
                    Date = valueDate,
                    Url = valueUrl
                };
                await _log.Log(newReques);
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = $"{user.FirstName}",
                LastName = $"{user.LastName}",
                JoinDate = DateTime.Now,
            };
            
           await _repo.AddUser(user);
            return View(user);
         // return Content($"Registration successful, {user.FirstName}");


        }

       






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
