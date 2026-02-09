using Microsoft.AspNetCore.Identity;

namespace EmailProject.Entities
{
    public class AppUser: IdentityUser
    {
        public string name { get; set; }
        public string Surname { get; set; }

        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public string? ConfirmCode { get; set; }



    }
}
