using System.ComponentModel.DataAnnotations;

namespace demoToken.API.Dto.form
{
    public class UtilisateurLoginForm
    {
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
