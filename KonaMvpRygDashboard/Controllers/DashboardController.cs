using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonaMvpRygDashboard.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IRainbowService _rainbowService;

        public DashboardController(IRainbowService rainbowService)
        {
            _rainbowService = rainbowService ?? throw new ArgumentNullException(nameof(rainbowService));
        }

        [HttpGet("directteam/{managerId}")]
        public IActionResult GetDirectTeamStatsByManagerId(string managerId)
        {
            IActionResult result;

            var teamStats = _rainbowService.GetDirectTeamStatsByManagerId(managerId);

            if (teamStats.Count() == 0)
            {
                result = new NotFoundResult();
            }
            else
            {
                result = new OkObjectResult(teamStats);
            }

            return result;
        }

        [HttpGet("fullteam/{managerId}")]
        public IActionResult GetMultiTeamStatsByManagerId(string managerId)
        {
            IActionResult result;

            var multiTeamStats = _rainbowService.GetMultiTeamStatsByManagerId(managerId);

            if (multiTeamStats.Count == 0)
            {
                result = new NotFoundResult();
            }
            else
            {
                result = new OkObjectResult(multiTeamStats);
            }

            return result;
        }
    }
}
