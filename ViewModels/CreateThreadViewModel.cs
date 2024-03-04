using ForumApp.Data.Enum;
using ForumApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ForumApp.ViewModels
{
    public class CreateThreadViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }

        public string AuthorId { get; set; }

        public int ForumId { get; set; }

    }
}