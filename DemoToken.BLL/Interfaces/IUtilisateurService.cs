using DemoToken.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoToken.BLL.Interfaces
{
    public  interface IUtilisateurService
    {
        void RegisterUtilisateur(UtilisateurModel utilisateur);

        UtilisateurModel LoginUtilisateur(string email, string password);
    }
}