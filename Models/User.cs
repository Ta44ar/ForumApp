using Microsoft.AspNetCore.Identity;

namespace ForumApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePicture { get; set; }
    }
}
