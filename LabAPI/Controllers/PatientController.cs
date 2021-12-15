using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Repository;
using LaboratoryAPI.Data.Model.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _db;
        public PatientController(IPatientRepository patientRepository)
        {
            _db = patientRepository;
        }

        /// <summary>
        /// Get all patients list
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            List<Patient> patients = _db.GetAllPatients();
            return Ok(patients);
        }

        /// <summary>
        /// Get Patient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetPatient(int id)
        {
            Patient patient = _db.GetPatient(id);
            return Ok(patient);
        }

        /// <summary>
        /// Get Patients list by filtered report type
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetByReportType")]
        public ActionResult<IEnumerable<Patient>> GetReports([FromQuery] ReportFilterRequest filter)
        {
            DateTime temp;
            if (DateTime.TryParse(filter.CreatedOnStart.ToString(), out temp) && DateTime.TryParse(filter.CreatedOnEnd.ToString(), out temp))
            {
                List<Patient> patients = _db.GetPatientsByFilter(filter);
                if (patients != null)
                {
                    return Ok(patients);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Create New Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatePatient([FromBody] PatientRequest patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _db.AddPatient(patient);

            return Ok();
        }

        /// <summary>
        /// Update Patient by Id
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult UpdatePatient(int id, PatientRequest patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Patient isPatientExist = _db.GetPatient(id);

            if (isPatientExist == null)
            {
                return NotFound();
            }
            _db.UpdatePatient(id, patient);

            return Ok();
        }

        /// <summary>
        /// Delete Patient By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeletePatient(int id)
        {
            Patient patient = _db.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            _db.DeletePatient(id);

            return Ok();
        }
    }
}
