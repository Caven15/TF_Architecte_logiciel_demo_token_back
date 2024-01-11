using demoToken.DAL.Data;
using DemoToken.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoToken.BLL.Mapper
{
    public static class Mapper
    {
        // BllToDal
        internal static UtilisateurData BllToDal(UtilisateurModel model)
        {
            return new UtilisateurData()
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance,
                Password = model.Password
            };
        }

        // DAlToBll
        internal static UtilisateurModel DalToBll(UtilisateurData data)
        {
            return new UtilisateurModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Prenom = data.Prenom,
                Email = data.Email,
                DateNaissance = data.DateNaissance,
                Password = data.Password
            };
        }
    }
}
