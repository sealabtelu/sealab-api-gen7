using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SealabAPI.Helpers
{
    public static class JwtHelper
    {
        public static dynamic ReadToken(this HttpRequest request, string property = null)
        {
            var bearer = request.Headers["Authorization"].ToString().Split(' ');
            if (bearer[0].ToLower() != "bearer")
                throw new ArgumentException("No prefix provided!");
            var token = bearer[1];
            return ReadToken(token, property);
        }
        public static dynamic ReadToken(string token, string property = null)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = tokenHandler.ReadJwtToken(token).Claims.Select(
                    c => new { c.Type, c.Value }
                );
                return property == null ? claims : claims.Where(c => c.Type == property).SingleOrDefault().Value;
            }
            catch { throw new ArgumentException("Invalid token!"); }
        }
        public static string CreateToken(Claim[] claims, DateTime expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("3a97556f-c07f-4f5a-8bc6-28711d4922e9");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expire,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
