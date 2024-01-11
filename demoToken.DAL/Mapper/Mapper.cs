using demoToken.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoToken.DAL.Mapper
{
    public static class Mapper
    {
        internal static UtilisateurData DbToDal(this IDataRecord record)
        {
            return new UtilisateurData()
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"],
                DateNaissance = (DateTime)record["DateNaissance"]
            };
        }
    }
}
