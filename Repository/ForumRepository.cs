using ForumApp.Data;
using ForumApp.Data.Enum;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Repository
{
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _context;
        public ForumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Forum forum)
        {
            _context.Add(forum);
            return Save();
        }

        public bool Delete(Forum forum)
        {
            _context.Remove(forum);
            return Save();
        }

        public async Task<IEnumerable<Forum>> GetAll()
        {
            return await _context.Forums.ToListAsync();
        }

        public async Task<IEnumerable<IGrouping<ForumCategory, Forum>>> GetAllGroupedByCategory()
        {
            var forumsGroupedByCategory = await _context.Forums
                .Include(f => f.Threads)
                    .ThenInclude(t => t.Posts)
                .GroupBy(f => f.ForumCategory)
                .ToListAsync();

            foreach (var group in forumsGroupedByCategory)
            {
                foreach (var forum in group)
                {
                    forum.ThreadCount = forum.Threads.Count;

                    forum.PostCount = forum.Threads.Sum(t => t.Posts?.Count ?? 0);
                }
            }

            return forumsGroupedByCategory;
        }

        public async Task<Forum> GetByIdAsync(int id)
        {
            return await _context.Forums.Include(i => i.Threads).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Forum forum)
        {
            _context.Update(forum);
            return Save();
        }
    }
}