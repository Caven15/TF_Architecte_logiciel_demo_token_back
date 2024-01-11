using demoToken.DAL.Interfaces;
using DemoToken.BLL.Interfaces;
using DemoToken.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoToken.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository

        public UtilisateurModel LoginUtilisateur(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterUtilisateur(UtilisateurModel utilisateur)
        {
            throw new NotImplementedException();
        }
    }
}
