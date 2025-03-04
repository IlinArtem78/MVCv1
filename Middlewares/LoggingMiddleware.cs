
using MVCv1.Controllers;
using MVCv1.Models;
using MVCv1.Repositoria;

public class LoggingMiddleware
    {
       

        private readonly RequestDelegate _next;
        
    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
           
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");

            string urlMessage = $"New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}  ";
            DateTime dateTimeUrl = DateTime.Now ;
            //для отобраения в файле склеиваем
            string logMessage = $"{dateTimeUrl} {urlMessage}"; 
            // Путь до лога (опять-таки, используем абсолютный путь либо Directory.GetCurrentDirectory())
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            
            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);

            //Используем своство IDictionary context для передачи в контроллер.  
            context.Items["CurrentDateTime"] = dateTimeUrl;
            context.Items["url"] = urlMessage;
            
           
        




        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
        }
            


    }

