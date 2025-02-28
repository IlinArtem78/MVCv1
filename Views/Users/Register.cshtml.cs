using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCv1.Models;
 

namespace MVCv1.Views.User
{
    public class RegisterModel : PageModel
    {
        // ссылка на контекст
        private readonly BlogContext _context;
        
        
       
        [HttpPost]
        public async Task<ContentResult> Register(MVCv1.Models.User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
            return Content($"Registration successful, {user.FirstName})");


        }
    }
}
