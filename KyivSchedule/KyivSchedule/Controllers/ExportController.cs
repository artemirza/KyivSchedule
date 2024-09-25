using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace KyivSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet]
        public async Task<IActionResult> ExportAllSchedules()
        {
            var schedules = await _exportService.ExportAllSchedulesAsync();

            return Ok(schedules);
        }

        [HttpGet("{groupNumber}")]
        public async Task<IActionResult> ExportScheduleByGroup(int groupNumber)
        {
            var schedule = await _exportService.ExportScheduleByGroupAsync(groupNumber);

            return Ok(schedule);
        }
    }
}
