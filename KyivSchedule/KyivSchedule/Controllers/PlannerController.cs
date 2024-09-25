using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KyivSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly IPlannerService _plannerService;

        public PlannerController(IPlannerService plannerService)
        {
            _plannerService = plannerService;
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or missing");
            await _plannerService.ImportFromFileAsync(file);
            return Ok("File imported successfully");
        }

        [HttpPut("{groupNumber}")]
        public async Task<IActionResult> UpdateOutageTimes(int groupNumber, [FromBody] List<TimeRangeDto> newOutageTimes)
        {

            await _plannerService.UpdateOutageTimesAsync(groupNumber, newOutageTimes);
            return Ok("The schedule has been updated successfully");
            

        }

        [HttpGet("{groupNumber}")]
        public async Task<IActionResult> CheckGroupStatus(int groupNumber)
        {
            bool isInOutage = await _plannerService.IsGroupInOutageNowAsync(groupNumber);

            if (isInOutage)
            {
                return Ok($"There are no lights on in group {groupNumber} right now.");
            }
            else
            {
                return Ok($"The {groupNumber} group now has light.");
            }
        }

        [HttpDelete("{groupNumber}")]
        public async Task<IActionResult> DeleteGroup(int groupNumber)
        {    
            await _plannerService.DeleteGroupAsync(groupNumber);
            return Ok($"The group with the number {groupNumber} has been successfully deleted.");
        }
    }
}
