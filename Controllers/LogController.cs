using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using MVCv1.Models;
using MVCv1.Repositoria;
using System.Diagnostics;

namespace MVCv1.Controllers
{
    public class LogController : Controller
    {
        private readonly ILog _logger;
      

        public LogController(ILog logger)
        {
            _logger = logger;
          
        }


        public async Task<IActionResult> Logs()
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

            var Logs = await _logger.GetLogs();
            return View(Logs);
        }

    }
 }



