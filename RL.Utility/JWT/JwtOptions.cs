using Microsoft.Extensions.Configuration;
using RL.Utility.Interfaces;
using RL.Share;
using System.Text;

namespace RL.Utility.JWT
{
    public class JwtOptions : IJwtOptions
    {
        private readonly IConfiguration _configuration;

        public JwtOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Issuer { get { return _configuration["Jwt:Issuer"]; } }
        public string Audience { get { return _configuration["Jwt:Audience"]; } }
        public byte[] SecretKey { get { return Encoding.ASCII.GetBytes(_configuration["Jwt:Key"].Base64Decode()); } }
    }
}
