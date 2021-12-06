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
    [Route ("api/[controller]")]
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
        public ActionResult<IEnumerable<Patient>> Get()
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
        public ActionResult Get(int id)
        {
            Patient patient = _db.GetPatient(id);
            return Ok(patient);
        }

        /// <summary>
        /// Get Patients list by filtered report type
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetByFilter")]
        public ActionResult<IEnumerable<Patient>> Get([FromQuery] ReportFilterRequest filter)
        {
            try {
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
                        return NotFound(ResponseMessage.RecordNotFound.ToString());
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create New Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] PatientRequest patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ResponseMessage.NotAValidRequest.ToString());
                }

                Patient savedPatient = _db.AddPatient(patient);

                return Ok(savedPatient);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update Patient by Id
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put(int id, PatientRequest patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ResponseMessage.NotAValidRequest.ToString());
                }
                string IsUpdated = _db.UpdatePatient(id, patient);

                if (IsUpdated == ResponseMessage.RecordNotFound.ToString())
                {
                    return NotFound(IsUpdated);
                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           

            return Ok(ResponseMessage.UpdatedSuccessfully.ToString());
        }

        /// <summary>
        /// Delete Patient By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string isDeleted = _db.DeletePatient(id);

            if (isDeleted == ResponseMessage.RecordNotFound.ToString())
            {
                return NotFound(isDeleted);
            }

            return Ok();
        }
    }
}
