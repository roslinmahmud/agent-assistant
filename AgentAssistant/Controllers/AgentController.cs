using AgentAssistant.HubConfig;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Domain.Repository;
using Domain.Models;
using Domain.DTO;

namespace AgentAssistant.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository agentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<DashboardHub> hubContext;
        private readonly HttpClient httpClient;
        private readonly TimerManager timerManager;
        private readonly IMapper mapper;

        public AgentController(IAgentRepository agentRepository,
            UserManager<ApplicationUser> userManager,
            IHubContext<DashboardHub> hubContext,
            TimerManager timerManager,
            IMapper mapper)
        {
            this.agentRepository = agentRepository;
            this.userManager = userManager;
            this.hubContext = hubContext;
            this.timerManager = timerManager;
            this.mapper = mapper;
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
        public async Task<IActionResult> GetAgent(int id)
        {
            var agent = await agentRepository.GetAgent(id);

            if (agent is null)
                return BadRequest("Agent not found");

            return Ok(agent);
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

        [HttpPost]
        public async Task<IActionResult> UpdateAgent([FromBody] Agent agent)
        {
            var entity = await agentRepository.GetAgent(agent.Id);
            if(entity == null)
                return BadRequest("Agent not found");

            entity = mapper.Map(agent, entity);

            await AuthenticateAgentCredentials(agent);

            agentRepository.UpdateAgent(entity);
            await agentRepository.SaveChangesAsync();

            return Created("api/Agent/", entity);
        }

        [HttpGet("summary/{id}")]
        public async Task<IActionResult> GetSummary(int id)
        {
            var agent = await agentRepository.GetAgent(id);

            if (agent is null)
                return BadRequest("Agent not found");

            if (agent.Email is null || agent.Password is null)
                return BadRequest("Agent credentials not found");

            timerManager.Action = async () => await hubContext.Clients.All.SendAsync("transferSummaryData", await GetDataFromServer(agent));
            timerManager.Timer.Change(0, 60000);

            return Ok(new { Message = "Request Completed" });
        }

        private async Task<IActionResult> GetDataFromServer(Agent agent)
        {
            await AuthenticateAgent(agent);

            var summary = new SummaryResponseDto
            {
                CurrentDayIncome = await GetCurrentDayIncome(),
                CurrentMonthIncome = await GetCurrentMonthIncome()
            };

            await GetCurrentDayStatement();

            return Ok(summary);
        }

        private async Task AuthenticateAgent(Agent agent)
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", agent.Email),
                new KeyValuePair<string, string>("password", agent.Password)
            });

            var response = await httpClient.PostAsync("login01.do", form);
            response.EnsureSuccessStatusCode();
        }
        
        private async Task<string> GetCurrentDayIncome()
        {
            var response = await httpClient.GetAsync("reports/commission02.do");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);
            var h3 = html.DocumentNode.SelectNodes("//h3");

            if (h3[0].InnerHtml == "No Transaction!")
                return "0.00";

            return Math.Round(float.Parse(h3[1].InnerHtml.Split(" ")[2]), 2).ToString();
        }

        private async Task<string> GetCurrentMonthIncome()
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("fromDate", DateTime.Now.AddDays(1-DateTime.Now.Day).Date.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("toDate", DateTime.Now.Date.ToString("yyyy-MM-dd"))
            });

            var response = await httpClient.PostAsync("reports/commission02.do", form);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);
            var h3 = html.DocumentNode.SelectNodes("//h3");

            return Math.Round(float.Parse(h3[1].InnerHtml.Split(" ")[2]), 2).ToString();
        }

        private async Task AuthenticateAgentCredentials(Agent agent)
        {
            var agentRequest = JsonSerializer.Serialize(new
            {
                userName = agent.Email,
                password = ComputeSHA256Hash(agent.Password).ToLower(),
                deviceMAC = ""
            });

            var content = new StringContent(
                agentRequest,
                Encoding.UTF8, 
                "application/json"
            );
            
            var response = await httpClient.PostAsync("inf/login.do", content);
            response.EnsureSuccessStatusCode();

            var contents = response.Content.ReadAsStringAsync();
        }

        public static string ComputeSHA256Hash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }

        private async Task<string> GetCurrentDayStatement()
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("format", "html"),
                new KeyValuePair<string, string>("fileName", "rangeWiseAccountStatement")
            });

            var response = await httpClient.PostAsync("reports/customer/general02.do", form);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);
            var tr = html.DocumentNode.SelectNodes("//tr");

            var text = tr[20].InnerText;

            string[] lines = text.Split("\n");

            return tr[0].ToString();
        }
    }
}
