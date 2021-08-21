using AuthModule.Business;
using AuthModule.Dto;
using AuthModule.Models;
using BaseModule.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AuthModule.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthBaseController : ActionBaseController<UserAccount>
    {
        private readonly AuthBusinessBase _businessBase;
        private readonly ILogger _logger;

        public AuthBaseController(AuthBusinessBase businessBase, ILogger logger) : base(businessBase, logger)
        {
            _businessBase = businessBase;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _businessBase.Login(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message ?? "While login operation, error occurred");
                return BadRequest(ex.Message ?? "While login operation, error occurred");
            }
        }

        [AllowAnonymous]
        [HttpPost("RefreshTokenLogin")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] string refreshToken)
        {
            try
            {
                var result = _businessBase.RefreshTokenLogin(1, "");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While login operation, error occurred");
                return BadRequest("While login operation, error occurred");
            }
        }

        [NonAction]
        public override Task<IActionResult> Update([FromBody] UserAccount entity)
        {
            return base.Update(entity);
        }

        [NonAction]
        public override Task<IActionResult> GetAll([FromBody] UserAccount entity = null)
        {
            return base.GetAll(entity);
        }
    }
}