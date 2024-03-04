

using ForumApp.Data.Enum;
using ForumApp.Models;

namespace ForumApp.Interfaces
{
    public interface IForumRepository
    {
        Task<IEnumerable<Forum>> GetAll();
        Task<IEnumerable<IGrouping<ForumCategory, Forum>>> GetAllGroupedByCategory();
        Task<Forum> GetByIdAsync(int id);
        bool Add(Forum forum);
        bool Update(Forum forum);
        bool Delete(Forum forum);
        bool Save();
    }
}
