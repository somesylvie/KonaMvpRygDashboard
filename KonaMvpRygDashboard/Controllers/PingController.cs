using KonaMvpRygDashboard.Database;
using Microsoft.AspNetCore.Mvc;

namespace KonaMvpRygDashboard.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            var message = $"{GetType().Namespace} successfully started!\n";
            
            using (var db = new SqliteDatabaseContext())
            {
                message += $"\nDatabase initialized with:\n\t{typeof(UserStatusEntry).FullName} {db.UserStatusEntries.Count()}";
            }

            return new OkObjectResult(message);
        }
    }
}