using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
