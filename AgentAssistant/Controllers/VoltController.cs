using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public IActionResult GetVolts()
        {
            try
            {
                var volts = voltRepository.GetAllVolts();

                return Ok(volts);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("{agentId}/{dateTime}")]
        public IActionResult GetVolt(string agentId, DateTime dateTime)
        {
            try
            {
                var volt = voltRepository.GetAllVolts()
                    .Where(v => v.AgentId == agentId && v.Date.Date == dateTime.Date)
                    .FirstOrDefault();

                return Ok(volt);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpPost]
        public IActionResult CreateVolt([FromBody] Volt volt)
        {
            try
            {
                voltRepository.CreateVolt(volt);

                if (voltRepository.SaveChanges() > 0)
                {
                    return CreatedAtAction("GetVolt", volt);
                }

                return BadRequest();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpPut]
        public IActionResult UpdateVolt([FromBody] Volt volt)
        {
            try
            {
                if (volt == null)
                {
                    return BadRequest("Volt object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Volt object");
                }

                var voltEntity = voltRepository.GetAllVolts()
                    .Where(v => v.Id == volt.Id)
                    .FirstOrDefault();
                if (voltEntity == null)
                {
                    return NotFound("Owner not found in server");
                }

                voltRepository.UpdateVolt(volt);
                voltRepository.SaveChanges();

                return NoContent();

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
