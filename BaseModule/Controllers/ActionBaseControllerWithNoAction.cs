using BaseModule.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [Route("[module]/[controller]")]
    [ApiController]
    public class ActionBaseController : ControllerBase
    {
        private ILogger _logger;

        public ActionBaseController( ILogger logger)
        {
            _logger = logger;
        }
    }
}
