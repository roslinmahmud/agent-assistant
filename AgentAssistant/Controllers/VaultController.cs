using Domain.Models;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VaultController : ControllerBase
    {
        private readonly IVaultRepository VaultRepository;

        public VaultController(IVaultRepository VaultRepository)
        {
            this.VaultRepository = VaultRepository;
        }

        [HttpGet("{agentId}/{date}")]
        public async Task<IActionResult> GetVault(int agentId, DateTime date)
        {
            var Vault = await VaultRepository.GetVaultAsync(agentId, date);

            return Ok(Vault);
        }

        [HttpGet("list/{agentId}/{date}")]
        public async Task<IActionResult> GetVaultList(int agentId, DateTime date)

        {
            var Vaults = await VaultRepository.GetVaultListAsync(agentId, date);

            return Ok(Vaults);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVault([FromBody] Vault Vault)
        {
            VaultRepository.CreateVault(Vault);
            await VaultRepository.SaveChangesAsync();

            return CreatedAtAction("CreateVault", Vault);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVault([FromBody] Vault Vault)
        {
            var VaultEntity = await VaultRepository.GetVaultByIdAsync(Vault.Id);

            if (VaultEntity == null)
            {
                return NotFound("Vault object not found in server");
            }

            VaultRepository.UpdateVault(Vault);
            await VaultRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
