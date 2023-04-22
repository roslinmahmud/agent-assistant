using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<string> GetJwtAccessTokenAsync(List<Claim> claims)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://dev-roslin.us.auth0.com/oauth/");

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", "AsfpzIm51U3LujDAIS0Fe2Wnw64rckTG"),
                new KeyValuePair<string, string>("client_secret", "iAk7TKwUjzgLaJA_mXQ0Zgk0ipiOSuj1fOp7Fry2K2x5RnKNtfu6ZIQW1f1GX7rq"),
                new KeyValuePair<string, string>("audience", "https://agentassistant.com")
            });

            var request = await client.PostAsync("token", form);

            request.EnsureSuccessStatusCode();

            var response = JsonSerializer.Deserialize<Token>(await request.Content.ReadAsStringAsync(), new JsonSerializerOptions());

            return response.AccessToken;
        }
    }

    class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
