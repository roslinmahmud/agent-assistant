using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public IActionResult GetVolt(int id)
        {
            try
            {
                var volt = voltRepository.GetAllVolts().Where(v => v.Id == id);

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
    }
}
