using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthModule.Controllers
{
    [Route("[module]/[controller]")]
    [ApiController]
    public class ActionBaseController : ControllerBase
    {
        private ILogger _logger;

        public ActionBaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}