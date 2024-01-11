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
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }

        public UtilisateurModel LoginUtilisateur(string email, string password)
        {
            return _utilisateurRepository.LoginUtilisateur(email, password)?.DalToBll();
        }

        public void RegisterUtilisateur(UtilisateurModel model)
        {
            _utilisateurRepository.RegisterUtilisateur(model.BllToDal());
        }
    }
}
