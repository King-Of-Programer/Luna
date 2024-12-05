using Luna_Edge.Data;
using Luna_Edge.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Luna_Edge.Services.Token
{
    public class TokenService
    {
        private readonly string _secretKey;

        public TokenService(IOptions<TokenSettings> options)
        {
            _secretKey = options.Value.SecretKey;
        }

        public string GenerateJwtToken(Model.User user)
        {
       
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
            
        };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = creds
            };

            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); 
        }
    }
}
