using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Hosting;

namespace ForumApp.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        [DefaultValue(false)]
        public bool Pinned { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public User Author { get; set; }

        [ForeignKey("Forum")]
        public int ForumId { get; set; }
        public Forum Forum { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

}
