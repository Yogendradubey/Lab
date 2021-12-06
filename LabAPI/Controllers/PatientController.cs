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

namespace LaboratoryAPI.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repo;
        public PatientController(IPatientRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all patients list
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            List<Patient> patients = _repo.GetAllPatients();
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
            Patient patient = _repo.GetPatient(id);
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
                    List<Patient> patients = _repo.GetPatientsByFilter(filter);
                    if (patients != null)
                    {
                        return Ok(patients);
                    }
                    else
                    {
                        return NotFound("Data not found");
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
        public ActionResult Post([FromQuery]PatientRequest patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid request");
                }

                Patient savedPatient = _repo.AddPatient(patient);

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
                    return BadRequest("Not a valid request");
                }
                string IsUpdated = _repo.UpdatePatient(id, patient);

                if (IsUpdated == "Record not found")
                {
                    return NotFound(IsUpdated);
                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           

            return Ok("Updated successfully");
        }

        /// <summary>
        /// Delete Patient By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string isDeleted = _repo.DeletePatient(id);

            if (isDeleted == "Record not found")
            {
                return NotFound(isDeleted);
            }

            return Ok();
        }
    }
}
