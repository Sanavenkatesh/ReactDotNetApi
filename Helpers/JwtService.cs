using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotNetApi.Helpers
{
    public class JwtService
    {
        private string SecurityKey = "this is a very secure key for generating jwt token";
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var credentails = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentails);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Now.AddDays(5));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken VerifyUser(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecurityKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters 
            { 
                IssuerSigningKey =  new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);
            
            return (JwtSecurityToken)validatedToken;
        }
    }
}
