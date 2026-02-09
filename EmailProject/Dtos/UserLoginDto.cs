using System.ComponentModel.DataAnnotations;

namespace EmailProject.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string username { get; set; }
        [Required(ErrorMessage = "Şifre zorunlu")]
        public string password { get; set; }
    }
}
