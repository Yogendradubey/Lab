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

    [Route("api/v{version:apiVersion}/LabTests")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class LabTestsController : ControllerBase
    {
        private readonly ILabTestRepository _testRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Lab Test Controller
        /// </summary>
        /// <param name="testRepo"></param>
        /// <param name="mapper"></param>
        public LabTestsController(ILabTestRepository testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of LabTests.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LabTests>))]
        public IActionResult GetLabTests()
        {
            var objList = _testRepo.GetLabTests();
            var objDto = new List<LabTestDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<LabTestDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual LabTest
        /// </summary>
        /// <param name="LabTestID"> The id of the Lab Test </param>
        /// <returns></returns>
        [HttpGet("{LabTestID:int}", Name = "GetLabTest")]
        [ProducesResponseType(200, Type = typeof(LabTestDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLabTest(int LabTestID)
        {
            var obj = _testRepo.GetLabTest(LabTestID);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<LabTestDto>(obj);

            return Ok(objDto);

        }

        /// <summary>
        /// Get Patients Lab Test
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PatientId:int}")]
        [ProducesResponseType(200, Type = typeof(LabTestDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetLabTestByPatient(int PatientId)
        {
            var objList = _testRepo.GetLabTestsByPatientId(PatientId);
            if (objList == null)
            {
                return NotFound();
            }
            var objDto = new List<LabTestDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<LabTestDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Add New Lab Test
        /// </summary>
        /// <param name="labTestsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LabTestDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLabTest([FromBody] LabTestDto labTestsDto)
        {
            if (labTestsDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_testRepo.LabTestExists(labTestsDto.LabTestName))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }
            var ObjlabTest = _mapper.Map<LabTests>(labTestsDto);
            if (!_testRepo.CreateLabTest(ObjlabTest))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ObjlabTest.LabTestName}");
                return StatusCode(500, ModelState);
            }
            return new ObjectResult(ObjlabTest) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Update Lab Test
        /// </summary>
        /// <param name="LabTestId"></param>
        /// <param name="labTestDto"></param>
        /// <returns></returns>
        [HttpPatch("{LabTestID:int}", Name = "UpdateLabTest")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLabTest(int LabTestId, [FromBody] LabTestDto labTestDto)
        {
            if (labTestDto == null || LabTestId != labTestDto.LabTestID)
            {
                return BadRequest(ModelState);
            }

            var labTestObj = _mapper.Map<LabTests>(labTestDto);
            if (!_testRepo.UpdateLabTest(labTestObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {labTestObj.LabTestName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete Existing Lab Test
        /// </summary>
        /// <param name="LabTestID"></param>
        /// <returns></returns>
        [HttpDelete("{LabTestID:int}", Name = "DeleteLabTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLabTest(int LabTestID)
        {
            if (!_testRepo.LabTestExists(LabTestID))
            {
                return NotFound();
            }

            var labTestObj = _testRepo.GetLabTest(LabTestID);
            if (!_testRepo.DeleteLabTest(labTestObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {labTestObj.LabTestName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
