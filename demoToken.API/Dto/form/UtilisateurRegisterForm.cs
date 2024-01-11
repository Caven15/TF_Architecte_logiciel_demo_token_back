using DemoToken.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace demoToken.API.Dto.form
{
    public class UtilisateurRegisterForm
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Nom { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Prenom { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Email { get; set; }
        [Required]
        public DateTime DateNaissance { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
