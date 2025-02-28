using Microsoft.AspNetCore.Mvc;
using MVCv1.Models;
using MVCv1.Repositoria;
using System.Diagnostics;

namespace MVCv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _repo;
        
        public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task <IActionResult> Index()
        {
            try { 
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
            catch (Exception ex) {
                
                Console.WriteLine(ex.ToString());
            }
            return View();

        }

        public async Task<IActionResult> Authors()
        {
            var authors = await _repo.GetUsers();

            Console.WriteLine("See all blog authors:");
            foreach (var author in authors)
                Console.WriteLine($"Author name {author.FirstName}, joined {author.JoinDate}");

            return View(authors);
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
    }
}
