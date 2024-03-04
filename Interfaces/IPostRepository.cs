namespace ForumApp.Interfaces
{
    public interface IPostRepository
    {
/*        Task<IEnumerable<Models.Thread>> GetAll();
        Task<Models.Thread> GetByIdAsync(int id);*/
        bool Add(Models.Post post);
/*        bool Update(Models.Thread thread);
        bool Delete(Models.Thread thread);*/
        bool Save();
    }
}
