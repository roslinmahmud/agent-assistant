using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
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
        public IActionResult GetStatements(string agentId, DateTime dateTime)
        {
            try
            {
                var statements = statementRepository.GetAllStatements()
                    .Where(s => s.AgentId == agentId && s.Date.Month == dateTime.Month && s.Date.Year == dateTime.Year);

                return Ok(statements);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public IActionResult CreateStatement([FromBody] Statement statement)
        {
            try
            {
                statementRepository.CreateStatement(statement);

                if (statementRepository.SaveChanges() > 0)
                {
                    return CreatedAtAction("GetStatement", statement);
                }

                return BadRequest();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("category/{agentId}")]
        public IActionResult GetStatementCategories(string agentId)
        {
            try
            {
                var statementcategories = statementCategoryRepository.GetAllStatementCategories()
                    .Where(a => a.AgentId == agentId);

                return Ok(statementcategories);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("category")]
        public IActionResult CreateStatementCategories(StatementCategory statementCategory)
        {
            try
            {
                statementCategoryRepository.CreateStatementCategory(statementCategory);

                if (statementCategoryRepository.SaveChanges() > 0)
                {
                    return CreatedAtAction("CreateStatementCategories", statementCategory);
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
