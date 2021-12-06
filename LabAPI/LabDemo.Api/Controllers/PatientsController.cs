using LabDemo.Models;
using LabDemo.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabDemo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;
        public PatientsController(ILogger<PatientsController> logger,IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }
        // GET: api/<PatientsController>
        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _patientService.GetPatients();
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public Patient Get(int id)
        {
            return _patientService.GetPatient(id);
        }
        /// <summary>
        /// Get Pataints with report type and date ranges
        /// </summary>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("{type}/{startDate}/{endDate}")]
        public IEnumerable<Patient> Get(TestTypeEnum type ,DateTime startDate,DateTime endDate)
        {
            return _patientService.GetPatients(type, startDate, endDate);
        }
        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] Patient patient)
        {
            _patientService.AddPatient(patient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Patient patient)
        {
            _patientService.UpdatePatient(patient);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _patientService.DeletePatient(id);
        }
    }
}
