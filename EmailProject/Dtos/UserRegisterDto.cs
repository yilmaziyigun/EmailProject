using System.ComponentModel.DataAnnotations;

namespace EmailProject.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Ad zorunludur")]
    public string name { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur")]
    public string surname { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
    public string username { get; set; }

    [Required(ErrorMessage = "Email zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email giriniz")]
    public string email { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
    public string password { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Kullanım şartlarını kabul etmelisiniz")]
    public bool AcceptTerms { get; set; }

    }
}
