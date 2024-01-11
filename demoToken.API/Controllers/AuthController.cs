// Importation des espaces de noms nécessaires
using demoToken.API.Dto.form;
using demoToken.API.Infrastructure;
using demoToken.API.Mapper;
using DemoToken.BLL.Interfaces;
using DemoToken.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// Déclaration d'un espace de noms pour le contrôleur
namespace demoToken.API.Controllers
{
    // L'attribut [Authorize] garantit que seuls les utilisateurs authentifiés peuvent accéder au contrôleur
    [Authorize]
    // Les attributs [Route] et [ApiController] définissent la routage et marquent la classe comme un contrôleur API
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Champs privés pour stocker les instances des services utilisés par le contrôleur
        private readonly IUtilisateurService _iutilisateurService;
        private readonly TokenManager _tokenManager;

        // Constructeur pour initialiser le contrôleur avec les services nécessaires
        public AuthController(IUtilisateurService iutilisateurService, TokenManager tokenManager)
        {
            _iutilisateurService = iutilisateurService;
            _tokenManager = tokenManager;
        }

        // Action pour l'inscription d'un utilisateur
        [HttpPost(nameof(Register))]
        public IActionResult Register(UtilisateurRegisterForm form)
        {
            try
            {
                // Vérifie si le modèle de données est valide
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                // Appelle le service pour enregistrer l'utilisateur
                _iutilisateurService.RegisterUtilisateur(form.ApiToBll());

                // Retourne une réponse de succès
                return Ok("Utilisateur enregistré avec succès !");
            }
            catch (Exception e)
            {
                // En cas d'erreur, retourne une réponse BadRequest avec le message d'erreur
                return BadRequest(e.Message);
            }
        }

        // Action pour la connexion d'un utilisateur
        [HttpPost(nameof(Login))]
        public IActionResult Login(UtilisateurLoginForm form)
        {
            try
            {
                // Vérifie si le modèle de données est valide
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Appelle le service pour la connexion de l'utilisateur
                UtilisateurModel currentUser = _iutilisateurService.LoginUtilisateur(form.Email, form.Password);

                // Vérifie si l'utilisateur existe
                if (currentUser is null)
                {
                    // Retourne une réponse NotFound si l'utilisateur n'existe pas
                    return NotFound("L'utilisateur n'existe pas...");
                }

                // Génère un jeton JWT en utilisant le TokenManager
                string token = _tokenManager.GerenateJwt(currentUser);

                // Retourne le jeton en cas de succès
                return Ok(token);
            }
            catch (Exception e)
            {
                // En cas d'erreur, retourne une réponse BadRequest avec le message d'erreur
                return BadRequest(e.Message);
            }
        }
    }
}
