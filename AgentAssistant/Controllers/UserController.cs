using AgentAssistant.JwtFeatures;
using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAgentRepository agentRepository;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtHandler<ApplicationUser> jwtHandler;

        public UserController(IAgentRepository agentRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            JwtHandler<ApplicationUser> jwtHandler)
        {
            this.agentRepository = agentRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            try
            {
                if (userForRegistrationDto == null)
                    return BadRequest();

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
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            var user = await userManager.FindByEmailAsync(userForAuthenticationDto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, userForAuthenticationDto.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Authentication failed. Wrong Username or Password" });


            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = await jwtHandler.GetClaims(user);
            var securityToken = jwtHandler.GetJwtSecurityToken(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

    }
}
