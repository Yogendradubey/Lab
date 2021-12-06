using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabDemo.Models;
using LabDemo.Services.LabReports;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabDemo.Api.Controllers
{
    [Authorize]
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
        public IEnumerable<LabReport> Get()
        {
            return _labReportService.GetLabReports();
        }

        // GET api/<LabReportsController>/5
        [HttpGet("{id}")]
        public LabReport Get(int id)
        {
            return _labReportService.GetLabReport(id);
        }

        // POST api/<LabReportsController>
        [HttpPost]
        public void Post([FromBody] LabReport labReport)
        {
            _labReportService.AddLabReport(labReport);
        }

        // PUT api/<LabReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] LabReport labReport)
        {
            _labReportService.UpdateLabReport(labReport);
        }

        // DELETE api/<LabReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _labReportService.DeleteLabReport(id);
        }
    }
}
