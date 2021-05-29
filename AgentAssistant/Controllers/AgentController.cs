using Entities.Models;
using Entities.Repository;
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

        public AgentController(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }
        [HttpGet]
        public IActionResult GetAgents()
        {
            var agents = agentRepository.GetAllAgents();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public string GetAgent(int id)
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
        public void Put(int id, [FromBody] Agent agent)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
