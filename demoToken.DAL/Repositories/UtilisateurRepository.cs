using demoToken.DAL.Data;
using demoToken.DAL.Interfaces;
using demoToken.DAL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace demoToken.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly Connection _connection;

        public UtilisateurRepository(Connection connection)
        {
            _connection = connection;
        }

        public UtilisateurData LoginUtilisateur(string email, string password)
        {
            Command command = new Command("SPUtilisateurLogin", true);

            command.AddParameter("Email", email);
            command.AddParameter("Password", password);

            return _connection.ExecuteReader(command, er => er.DbToDal()).SingleOrDefault();
        }

        public void RegisterUtilisateur(UtilisateurData data)
        {
            Command command = new Command("SPUtilisateurRegister", true);

            command.AddParameter("Nom", data.Nom);
            command.AddParameter("Prenom", data.Prenom);
            command.AddParameter("Email", data.Email);
            command.AddParameter("DateNaissance", data.DateNaissance);
            command.AddParameter("Password", data.Password);

            _connection.ExecuteNonQuery(command);

        }
    }
}