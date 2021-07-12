using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository agentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AgentController(IAgentRepository agentRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.agentRepository = agentRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await agentRepository.GetAllAgents();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public string GetAgent(string id)
        {
            return "value";
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateAgent([FromBody] Agent agent, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
                return BadRequest("Object is null");


            agentRepository.CreateAgent(agent);
            await agentRepository.SaveChangesAsync();

            user.AgentId = agent.Id;
            await agentRepository.SaveChangesAsync();

            return Created("api/Agent/", agent);

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
