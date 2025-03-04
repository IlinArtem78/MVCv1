using Microsoft.EntityFrameworkCore;
using MVCv1.Models;

namespace MVCv1.Repositoria
{
    public class LogRepositoria : ILog
    {
        // ссылка на контекст
        private readonly BlogContext _context;
       

        public LogRepositoria(BlogContext context)
        {
            _context = context;
        }
        public async Task Log(Request request)
        {

            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);
            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
        public async Task<Request[]> GetLogs()
        {
            // Получим все логи
            return await _context.Requests.ToArrayAsync();
        }
    }
}
