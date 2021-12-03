using System;
using System.Collections.Generic;
using AutoMapper;
using LabAPI.Models;
using LabAPI.Models.Dtos;
using LabAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Patients")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PatientDetailsController : ControllerBase
    {
        private readonly IPatientDetails _npRepo;
        private readonly IMapper _mapper;

        public PatientDetailsController(IPatientDetails npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of Patients.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PatientDetailsDto>))]
        public IActionResult GetPatientDetails()
        {
            var objList = _npRepo.GetPatientDetails();
            var objDto = new List<PatientDetailsDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PatientDetailsDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual Patient Detail
        /// </summary>
        /// <param name="PatientID"> The Id of the Patient </param>
        /// <returns></returns>
        [HttpGet("{PatientID:int}", Name = "GetPatientDetail")]
        [ProducesResponseType(200, Type = typeof(PatientDetailsDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetPatientDetail(int PatientID)
        {
            var obj = _npRepo.GetPatientDetail(PatientID);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<PatientDetailsDto>(obj);
            return Ok(objDto);

        }
        /// <summary>
        /// Add New Patient
        /// </summary>
        /// <param name="patientDetailsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PatientDetailsDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddPatientDetails([FromBody] PatientDetailsDto patientDetailsDto)
        {
            if (patientDetailsDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_npRepo.PatientExists(patientDetailsDto.PatientID))
            {
                ModelState.AddModelError("", "Record Exists!");
                return StatusCode(404, ModelState);
            }
            var PatientObj = _mapper.Map<PatientDetails>(patientDetailsDto);
            if (!_npRepo.CreatePatientDetail(PatientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {PatientObj.PatientName}");
                return StatusCode(500, ModelState);
            }
            return new ObjectResult(PatientObj) { StatusCode = StatusCodes.Status201Created };
        }
        /// <summary>
        /// Update Patient Details
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="patientDetailsDto"></param>
        /// <returns></returns>
        [HttpPatch("{PatientID:int}", Name = "UpdatePatientDetails")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatientDetails(int PatientID, [FromBody] PatientDetailsDto patientDetailsDto)
        {
            if (patientDetailsDto == null || PatientID != patientDetailsDto.PatientID)
            {
                return BadRequest(ModelState);
            }

            var PatientObj = _mapper.Map<PatientDetails>(patientDetailsDto);
            if (!_npRepo.UpdatePatientDetails(PatientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {PatientObj.PatientName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete Patient Entry
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        [HttpDelete("{PatientID:int}", Name = "DeletePatientDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePatientDetails(int PatientID)
        {
            if (!_npRepo.PatientExists(PatientID))
            {
                return NotFound();
            }

            var patientObj = _npRepo.GetPatientDetail(PatientID);
            if (!_npRepo.DeletePatientDetail(patientObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {patientObj.PatientName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



    }
}
