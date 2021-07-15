using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgentAssistant.JwtFeatures
{
    public class JwtHandler<T> where T: ApplicationUser
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<T> userManager;

        public JwtHandler(IConfiguration configuration,
            UserManager<T> userManager)
        {
            _configuration = configuration;
            this.userManager = userManager;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<List<Claim>> GetClaims(T user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.AgentId.ToString())
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public JwtSecurityToken GetJwtSecurityToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var securityToken = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:validIssuer"],
                audience:_configuration["JwtSettings:validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return securityToken;
        }
    }
}
