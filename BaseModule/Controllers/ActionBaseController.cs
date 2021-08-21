using BaseModule.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseModule.Controllers
{
    [Route("[module]/[controller]")]
    public class ActionBaseController<T> : Controller
    where T : class, IEntity, new()
    {
        private readonly IBusinessBase<T> _businessBase;
        private ILogger _logger;

        public ActionBaseController(IBusinessBase<T> businessBase, ILogger logger)
        {
            _businessBase = businessBase;
            _logger = logger;
        }

        [HttpPost("GetByModel")]
        public virtual async Task<IActionResult> Get([FromBody] T entity)
        {
            try
            {
                var result = await _businessBase.GetAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While getting record according to by filter, error occurred");
                return BadRequest("While getting record according to by filter, error occurred");
            }
        }

        [HttpGet("GetById")]
        public virtual async Task<IActionResult> Find([FromQuery] int id)
        {
            try
            {
                var result = await _businessBase.FindAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While getting record according to by Id, error occurred");
                return BadRequest("While getting record according to by Id, error occurred");
            }
        }

        [HttpPost("GetAll")]
        public virtual async Task<IActionResult> GetAll([FromBody] T entity = null)
        {
            try
            {
                var result = await _businessBase.GetAllAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While getting records according to by filter, error occurred");
                return BadRequest("While getting records according to by filter, error occurred");
            }
        }

        [HttpPost("Add")]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            try
            {
                var result = await _businessBase.InsertAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While inserting a new record, error occurred");
                return BadRequest("While inserting a new record, error occurred");
            }
        }

        [HttpPut("Update")]
        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            try
            {
                var result = await _businessBase.UpdateAsync(entity);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While updating record, error occurred");
                return BadRequest("While updating record, error occurred");
            }
        }

        [HttpDelete("RemoveByModel")]
        public virtual async Task<IActionResult> Remove([FromBody] T model)
        {
            try
            {
                var result = await _businessBase.DeleteAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While removing record by model, error occurred");
                return BadRequest("While removing record by model, error occurred");
            }
        }

        [HttpDelete("RemoveById")]
        public virtual async Task<IActionResult> Remove([FromBody] int Id)
        {
            try
            {
                var result = await _businessBase.DeleteAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While removing record by id, error occurred");
                return BadRequest("While removing record by id, error occurred");
            }
        }

        [HttpDelete("RemoveByIds")]
        public virtual async Task<IActionResult> Remove([FromBody] List<int> Ids)
        {
            try
            {
                var result = await _businessBase.DeleteAsync(Ids);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While removing records by id, error occurred");
                return BadRequest("While removing records by id, error occurred");
            }
        }

        [HttpDelete("RemoveByModels")]
        public virtual async Task<IActionResult> Remove([FromBody] List<T> entities)
        {
            try
            {
                var result = await _businessBase.DeleteAsync(entities);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "While removing records by models, error occurred");
                return BadRequest("While removing records by models, error occurred");
            }
        }
    }
}