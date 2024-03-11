using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RL.Entity.DTOs;
using RL.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RL.Utility.JWT
{
    public class JwtProvider : IJwtProvider
    {

        private readonly IConfiguration _configuration;
        private readonly IJwtOptions _jwtOptions;

        public JwtProvider(IConfiguration configuration, IJwtOptions jwtOptions)
        {
            _configuration = configuration;
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(AuthResponse user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("DisplayName", user.DisplayName),
                new Claim("SessionId", "0"),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)

            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddHours(8),
                claims: claims,
                signingCredentials: new SigningCredentials(
                                                        new SymmetricSecurityKey(_jwtOptions.SecretKey),
                                                            SecurityAlgorithms.HmacSha256
                                                          )
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
