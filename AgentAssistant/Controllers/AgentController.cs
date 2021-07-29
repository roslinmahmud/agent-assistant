using Entities.DTO;
using Entities.Models;
using Entities.Repository;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository agentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly HttpClient httpClient;

        public AgentController(IAgentRepository agentRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.agentRepository = agentRepository;
            this.userManager = userManager;

            HttpClientHandler httpClientHandler = new() { CookieContainer = new CookieContainer()};
            HttpClient httpClient = new(httpClientHandler)
            {
                BaseAddress = new Uri("https://agent.islamibankbd.com/"),
                Timeout = new TimeSpan(0, 0, 30)
            };
            this.httpClient = httpClient;
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
        
        private async Task Login()
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "nasrinnaharhena@gmail.com"),
                new KeyValuePair<string, string>("password", "Unlock1971")
            });

            var response = await httpClient.PostAsync("login01.do", form);
            response.EnsureSuccessStatusCode();
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            await Login();

            var summary = new SummaryResponseDto
            {
                CurrentDayIncome = Math.Round(float.Parse(await GetCurrentDayIncome()), 2).ToString(),
                CurrentMonthIncome = Math.Round(float.Parse(await GetCurrentMonthIncome()), 2).ToString()
            };

            return Ok(summary);
        }

        private async Task<string> GetCurrentDayIncome()
        {
            var response = await httpClient.GetAsync("reports/commission02.do");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);

            var body = html.DocumentNode.SelectSingleNode("//body");
            var h3 = body.SelectNodes("//h3");

            return h3[1].InnerHtml.Split(" ")[2];
        }

        private async Task<string> GetCurrentMonthIncome()
        {
            var date = DateTime.Now;

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("fromDate", new DateTime(date.Year, date.Month, 1).Date.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("toDate", date.Date.ToString("yyyy-MM-dd"))
            });

            var d  = new DateTime(date.Year, date.Month, 1).Date.ToShortDateString();
            var response = await httpClient.PostAsync("reports/commission02.do", form);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);

            var body = html.DocumentNode.SelectSingleNode("//body");
            var h3 = body.SelectNodes("//h3");

            return h3[1].InnerHtml.Split(" ")[2];
        }


    }
}
