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
    public class StatementController : Controller
    {
        private readonly IStatementRepository statementRepository;
        private readonly IStatementCategoryRepository statementCategoryRepository;

        public StatementController(IStatementRepository statementRepository,
            IStatementCategoryRepository statementCategoryRepository)
        {
            this.statementRepository = statementRepository;
            this.statementCategoryRepository = statementCategoryRepository;
        }

        [HttpGet("{agentId}/{dateTime}")]
        public async Task<IActionResult> GetStatements(string agentId, DateTime dateTime)
        {
            try
            {
                var statements = await statementRepository.GetAllStatementsAsync(agentId, dateTime);
                    
                return Ok(statements);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateStatement([FromBody] Statement statement)
        {
            try
            {
                if (statement == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid Statement object");
                }

                statementRepository.CreateStatement(statement);

                await statementRepository.SaveChangesAsync();

                return CreatedAtAction("GetStatement", statement);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }
            
        }

        [HttpGet("category/{agentId}")]
        public async Task<IActionResult> GetStatementCategories(string agentId)
        {
            try
            {
                var statementCategories = await statementCategoryRepository.GetAllStatementCategoriesAsync(agentId);

                return Ok(statementCategories);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }

        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateStatementCategory(StatementCategory statementCategory)
        {
            try
            {
                if (statementCategory == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid StatementCategory object");
                }

                statementCategoryRepository.CreateStatementCategory(statementCategory);

                await statementCategoryRepository.SaveChangesAsync();

                CreatedAtAction("CreateStatementCategory", statementCategory);

                return BadRequest();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error: " + e.Message);
            }

        }
    }
}
