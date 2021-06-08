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
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository agentRepository;
        private readonly UserManager<Agent> userManager;
        private readonly IMapper mapper;
        private readonly JwtHandler<Agent> jwtHandler;

        public AgentController(IAgentRepository agentRepository,
            UserManager<Agent> userManager,
            IMapper mapper,
            JwtHandler<Agent> jwtHandler)
        {
            this.agentRepository = agentRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            try
            {
                if (userForRegistrationDto == null)
                    return BadRequest();

                var user = mapper.Map<Agent>(userForRegistrationDto);

                var result = await userManager.CreateAsync(user, userForRegistrationDto.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }

                await userManager.AddToRoleAsync(user, "Agent");

                return StatusCode(201);
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

        [HttpGet]
        public IActionResult GetAgents()
        {
            var agents = agentRepository.GetAllAgents();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public string GetAgent(string id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CreateAgent([FromBody] Agent agent)
        {
            if (agent == null)
                return BadRequest();

            agentRepository.CreateAgent(agent);

            agentRepository.SaveChanges();

            return Created("api/Agent/",agent);

        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Agent agent)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
