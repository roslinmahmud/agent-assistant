using AgentAssistant.JwtFeatures;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtHandler<ApplicationUser> jwtHandler;

        public UserController(IMapper mapper,
            UserManager<ApplicationUser> userManager,
            JwtHandler<ApplicationUser> jwtHandler)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var user = mapper.Map<ApplicationUser>(userForRegistrationDto);

            var result = await userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            await userManager.AddToRoleAsync(user, "Agent");

            return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true, UserId = user.Id });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            var user = await userManager.FindByEmailAsync(userForAuthenticationDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, userForAuthenticationDto.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Authentication failed. Wrong Username or Password" });


            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = await jwtHandler.GetClaims(user);
            var securityToken = jwtHandler.GetJwtSecurityToken(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

    }
}
