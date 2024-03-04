using ForumApp.Data;
using ForumApp.Interfaces;
using ForumApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Repository
{
    public class ThreadRepository : IThreadRepository
    {
        private readonly ApplicationDbContext _context;

        public ThreadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Models.Thread thread)
        {
            _context.Add(thread);
            return Save();
        }

        public bool Delete(Models.Thread thread)
        {
            _context.Remove(thread);
            return Save();
        }

        public async Task<IEnumerable<Models.Thread>> GetAll()
        {
            return await _context.Threads.ToListAsync();
        }

        public async Task<Models.Thread> GetByIdAsync(int id)
        {
            return await _context.Threads.Include(i => i.Author).Include(i => i.Posts).ThenInclude(t => t.Author).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Pinned(int id, bool pinned)
        {
            _context.Threads
                .Where(t => t.Id == id)
                .ExecuteUpdate(b =>
                    b.SetProperty(t => t.Pinned, pinned)
                );

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool Update(Models.Thread thread)
        {
            _context.Update(thread);
            return Save();
        }
    }
}
