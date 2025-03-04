using MVCv1.Models;

namespace MVCv1.Repositoria
{
    public interface ILog
    {
        Task Log(Request request);

        Task<Request[]> GetLogs();
    }
}
