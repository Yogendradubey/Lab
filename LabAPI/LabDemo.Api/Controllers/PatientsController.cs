﻿using LabDemo.Models;
using LabDemo.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabDemo.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult Get()
        {
            return Ok( _patientService.GetPatients());
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 0)
            {
                return Ok(_patientService.GetPatient(id));
            }
            return BadRequest();
            
        }
        /// <summary>
        /// Get Pataints with report type and date ranges
        /// </summary>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("{type}/{startDate}/{endDate}")]
        public IActionResult Get(TestTypeEnum type ,DateTime startDate,DateTime endDate)
        {
            return Ok( _patientService.GetPatients(type, startDate, endDate));
        }
        // POST api/<PatientsController>
        [HttpPost]
        public IActionResult Post([FromBody] Patient patient)
        {
            _patientService.AddPatient(patient);
            return Ok();
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Patient patient)
        {
            _patientService.UpdatePatient(patient);
            return Ok( );
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id>0)
            {
                _patientService.DeletePatient(id);
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
