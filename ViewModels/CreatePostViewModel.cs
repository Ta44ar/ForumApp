using ForumApp.Data.Enum;
using ForumApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ForumApp.ViewModels
{
    public class CreatePostViewModel
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
        public int ThreadId { get; set; }
    }
}