using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RazorPagesUI.Api.Controllers.Auth
{
    public static class JwtTokenFactory
    {
        public static string Issuer => "DemoApi";
        public static string Audience => "DemoApi";
        public static string Key => "super_secret_demo_key_change_me_1234567890";

        public static string Create(string username, string role, int expiresMinutes = 15)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, role),
                new("demo", "true")
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}