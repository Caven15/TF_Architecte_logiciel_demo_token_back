using DemoToken.BLL.Models;
using demoToken.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mapper
{
    public static class Mapper
    {
        internal static UtilisateurData BllToDal(this UtilisateurModel model)
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

        internal static UtilisateurModel DalToBll(this UtilisateurData data)
        {
            if (data is null)
            {
                return null;
            }
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
