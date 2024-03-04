using ForumApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Image { get; set; }

        public ForumCategory ForumCategory { get; set; }
        public ICollection<Thread> Threads { get; set; }

        [NotMapped] // This property is not stored in the database
        public int ThreadCount { get; set; }


        [NotMapped] // This property is not stored in the database
        public int PostCount { get; set; }
    }
}
