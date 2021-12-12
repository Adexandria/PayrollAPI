using EmployeeAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class Credentials
    {
        readonly IConfiguration _config;
        public Credentials(IConfiguration _config)
        {
            this._config = _config;
       }
        private IConfigurationSection GetSection()
        {
            return _config.GetSection("JwtSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {
            var _jwtSettings = GetSection();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,Employee employee)
        {
            var _jwtSettings = GetSection();
            var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.GetSection("validIssuer").Value,
            audience: _jwtSettings.GetSection("validAudience").Value,
            claims:GetClaims(employee),
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);
            return tokenOptions;
        }
        public List<Claim> GetClaims(Employee employee)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email,employee.Email)
            };
            claims.Add(new Claim(ClaimTypes.Role, employee.Role));
            return claims;
        }
    }
}
