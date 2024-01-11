using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demoToken.API.Infrastructure
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;
        public readonly string _secret;
        public readonly string _issuer;
        public readonly string _audience;

        public TokenManager(IConfiguration configuration)
        {
            // initialisation de ma classe avec la configuration fournie
            _configuration = configuration;
            _secret = _configuration["jwt:key"];
            _issuer = _configuration["jwt:issuer"];
            _audience = _configuration["jwt:audience"];
        }

        public string GerenateJwt(dynamic user, int expirationDate = 1)
        {
            // Création des crédentials pour signer le token
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Obtention de l'heure actuelle
            DateTime now = DateTime.Now;

            // Création des revendications (Claims) pour le stocker dans le token
            Claim[] myClaims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),

                new Claim(ClaimTypes.GivenName, $"{user.Nom} {user.Prenom}"),

                new Claim(ClaimTypes.Expiration, now.Add(TimeSpan.FromDays(expirationDate)).ToString(), ClaimValueTypes.DateTime)
            };

            // Génération du token en utilisant la bibliotheque => System.IdentotyModel.Tokens.jwt
            JwtSecurityToken token = new JwtSecurityToken(
                claims : myClaims,
                expires : now.Add(TimeSpan.FromDays(expirationDate)),
                signingCredentials : credentials,
                issuer : _issuer,
                audience : _audience
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Écriture du token en tant que chaine de caractère
            return tokenHandler.WriteToken(token);
        }
    }
}
