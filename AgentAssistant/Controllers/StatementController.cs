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
        public async Task<IActionResult> GetStatements(int agentId, DateTime dateTime)
        {
            var statements = await statementRepository.GetAllStatementsAsync(agentId, dateTime);

            return Ok(statements);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatement([FromBody] Statement statement)
        {
            statementRepository.CreateStatement(statement);
            await statementRepository.SaveChangesAsync();

            return CreatedAtAction("CreateStatement", statement);
        }

        [HttpGet("category/{agentId}")]
        public async Task<IActionResult> GetStatementCategories(int agentId)
        {
            var statementCategories = await statementCategoryRepository.GetAllStatementCategoriesAsync(agentId);

            return Ok(statementCategories);
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateStatementCategory(StatementCategory statementCategory)
        {
            statementCategoryRepository.CreateStatementCategory(statementCategory);
            await statementCategoryRepository.SaveChangesAsync();

            return CreatedAtAction("CreateStatementCategory", statementCategory);
        }
    }
}
