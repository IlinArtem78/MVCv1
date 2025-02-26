using MVCv1.Models;

namespace MVCv1.Repositoria
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
