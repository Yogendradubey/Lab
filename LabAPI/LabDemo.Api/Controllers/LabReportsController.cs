using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabDemo.Models;
using LabDemo.Services.LabReports;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabDemo.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LabReportsController : ControllerBase
    {
        private readonly ILogger<LabReportsController> _logger;
        private readonly ILabReportService _labReportService;
        public LabReportsController(ILogger<LabReportsController> logger,
            ILabReportService labReportService)
        {
            _logger = logger;
            _labReportService = labReportService;
        }
        // GET: api/<LabReportsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_labReportService.GetLabReports());
        }

        // GET api/<LabReportsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 0)
            {
                return Ok(_labReportService.GetLabReport(id));
            }
            return BadRequest();
        }

        // POST api/<LabReportsController>
        [HttpPost]
        public IActionResult Post([FromBody] LabReport labReport)
        {
            _labReportService.AddLabReport(labReport);
            return Ok();
        }

        // PUT api/<LabReportsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LabReport labReport)
        {
            _labReportService.UpdateLabReport(labReport);
            return Ok();
        }

        // DELETE api/<LabReportsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _labReportService.DeleteLabReport(id);
                return Ok();
            }
            return BadRequest();

        }
    }
}
