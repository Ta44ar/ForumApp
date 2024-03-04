using ForumApp.Data.Enum;
using ForumApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ForumApp.ViewModels
{
    public class PinThreadViewModel
    {
        public int Id { get; set; }
        public bool Pinned { get; set; }
        public int ForumId { get; set; }
    }
}