using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VY.Rebeld.Business.Contracts.Services;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;

namespace VY.Rebeld.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebeldController : ControllerBase
    {
        private readonly IRebeldService _rebeldService;

        public RebeldController(IRebeldService rebeldService)
        {
            _rebeldService = rebeldService;
        }

        /// <summary>
        /// Returns last Rebeld sighting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Registry of last sighting for that rebeld in format **_rebeld (name) on (planet) at (datetime)_**</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var result = await _rebeldService.GetRebeldSightingAsync(name);

            if (result.HasErrors())
            {
                var errors = result.GetAllErrors();

                var badRequestErrors = errors.Where(c => c.Code == 400);

                if (errors.Where(c => c.Code == 204).Any())
                {
                    return NoContent();
                }
                if (badRequestErrors.Any())
                {
                    return BadRequest(badRequestErrors.First().Message);
                }
                if (errors.Where(c => c.Code == 404).Any())
                {
                    return StatusCode(500, "Rebeld data file not found on server");
                }
            }

            return Ok(result.Result.SightText);
        }

        /// <summary>
        /// Saves a rebeld to the DB with format <_rebeld (name) on (planet) at (datetime)_>
        /// </summary>
        /// <param name="rebeldDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ICollection<string>))]
        public async Task<IActionResult> PostRebeld([FromBody] RebeldDto rebeldDto)
        {
            var result = await _rebeldService.SaveRebeldAsync(rebeldDto);

            if (result.HasErrors())
            {
                return StatusCode(500, result.GetAllErrors().Select(c => string.Format("Code: {0} Message: {1}", c.Code, c.Message)).ToList());
            }

            return Ok();
        }
    }
}
