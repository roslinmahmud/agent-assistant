using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public async Task<IActionResult> GetVolt(string agentId, DateTime date)
        {
            try
            {
                var volt = await voltRepository.GetVoltAsync(agentId, date);

                return Ok(volt);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }

        }

        [HttpGet("list/{agentId}/{date}")]
        public async Task<IActionResult> GetVoltList(string agentId, DateTime date)
        {
            try
            {
                var volts = await voltRepository.GetVoltListAsync(agentId, date);

                return Ok(volts);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateVolt([FromBody] Volt volt)
        {
            try
            {
                if (volt == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid Volt object");
                }

                voltRepository.CreateVolt(volt);

                await voltRepository.SaveChangesAsync();

                return CreatedAtAction("GetVolt", volt);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVolt([FromBody] Volt volt)
        {
            try
            {
                if (volt == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid Volt object");
                }

                var voltEntity = await voltRepository.GetVoltByIdAsync(volt.Id);

                if (voltEntity == null)
                {
                    return NotFound("Volt object not found in server");
                }

                voltRepository.UpdateVolt(volt);

                await voltRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
    }
}
