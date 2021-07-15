using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VoltController : ControllerBase
    {
        private readonly IVoltRepository voltRepository;

        public VoltController(IVoltRepository voltRepository)
        {
            this.voltRepository = voltRepository;
        }

        [HttpGet("{agentId}/{date}")]
        public async Task<IActionResult> GetVolt(int agentId, DateTime date)
        {
            var volt = await voltRepository.GetVoltAsync(agentId, date);

            return Ok(volt);
        }

        [HttpGet("list/{agentId}/{date}")]
        public async Task<IActionResult> GetVoltList(int agentId, DateTime date)
        {
            var volts = await voltRepository.GetVoltListAsync(agentId, date);

            return Ok(volts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVolt([FromBody] Volt volt)
        {
            voltRepository.CreateVolt(volt);
            await voltRepository.SaveChangesAsync();

            return CreatedAtAction("CreateVolt", volt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVolt([FromBody] Volt volt)
        {
            var voltEntity = await voltRepository.GetVoltByIdAsync(volt.Id);

            if (voltEntity == null)
            {
                return NotFound("Volt object not found in server");
            }

            voltRepository.UpdateVolt(volt);
            await voltRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
