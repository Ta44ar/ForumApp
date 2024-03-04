using ForumApp.Data.Enum;
using ForumApp.Models;


namespace ForumApp.ViewModels
{
    public class CreateForumViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ForumCategory ForumCategory { get; set; }
    }
}