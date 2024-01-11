using DemoToken.BLL.Models;
using demoToken.DAL.Data;
using demoToken.API.Dto.form;

namespace demoToken.API.Mapper
{
    public static class Mapper
    {
        internal static UtilisateurModel ApiToBll(this UtilisateurRegisterForm form)
        {
            return new UtilisateurModel()
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                Email = form.Email,
                DateNaissance = form.DateNaissance,
                Password = form.Password
            };
        }
    }
}
