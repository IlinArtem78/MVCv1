using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCv1.Models;
using MVCv1.Repositoria;
using System.Diagnostics;

namespace MVCv1.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ILog _logger;

        public FeedbackController(ILog logger)
        {
            _logger = logger;
        }




        /// <summary>
        ///  Метод, возвращающий страницу с отзывами
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Add()
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
                await _logger.Log(newReques);
            }
            return View();
        }

        /// <summary>
        /// Метод для Ajax-запросов
        /// </summary>
        [HttpPost]
        public IActionResult Add(Feedback feedback)
        {
            return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
