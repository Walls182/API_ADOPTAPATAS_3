using API_ADOPTAPATAS_3.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_ADOPTAPATAS_3.Utility
{
    public class JwtUtility
    {
        public static ResponseLoginDto? GenToken(ResponseLoginDto userToken, JwtSettingsDto jwtSettings)
        {
            if (userToken == null)
            {
                throw new ArgumentException("El objeto 'userToken' no puede ser nulo.");
            }

            if (jwtSettings == null)
            {
                throw new ArgumentException("El objeto 'jwtSettings' no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(jwtSettings.Key))
            {
                throw new ArgumentException("La clave 'Key' en 'jwtSettings' no puede ser nula o vacía.");
            }

            DateTime expireTime;

            if (jwtSettings.FlagExpirationTimeHours)
            {
                expireTime = DateTime.Now.AddHours(jwtSettings.ExpirationTimeHours);
            }
            else if (jwtSettings.FlagExpirationTimeMinutes)
            {
                expireTime = DateTime.Now.AddMinutes(jwtSettings.ExpirationTimeMinutes);
            }
            else
            {
                return null;
            }

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            var claims = new List<Claim> { new Claim("TiempoExpiracion", expireTime.ToString("yyyy-MM-dd HH:mm")) };

            var JWToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expireTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            userToken.TiempoExpiracion = expireTime;

            return userToken;
        }
      

    }
}
