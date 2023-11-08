using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Dtos.Responses;
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
            try
            {


                if (userToken == null) throw new ArgumentException(nameof(userToken));
                var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
                DateTime expireTime = DateTime.Now;

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

                var claims = new List<Claim> { new Claim("TiempoExpiracion", expireTime.ToString("yyyy-MM-dd HH:mm")) };

                var JWToken = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                userToken.TiempoExpiracion = expireTime;

                return userToken;
            }

            catch (Exception)
            {

                return null;
            }

        }
    }
}
