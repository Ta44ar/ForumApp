using ForumApp.Data.Enum;
using ForumApp.Models;

namespace ForumApp.Interfaces
{
    public interface IThreadRepository
    {
        Task<IEnumerable<Models.Thread>> GetAll();
        Task<Models.Thread> GetByIdAsync(int id);
        bool Add(Models.Thread thread);
        public bool Pinned(int id, bool pinned);
        bool Update(Models.Thread thread);
        bool Delete(Models.Thread thread);
        bool Save();
    }
}
