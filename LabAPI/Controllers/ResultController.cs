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
    [Route("api/v{version:apiVersion}/Results")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ResultController : ControllerBase
    {
        private readonly ITestResultRepository _testRepo;
        private readonly IMapper _mapper;

        public ResultController(ITestResultRepository testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of Test Result.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TestResult>))]
        public IActionResult GetResults()
        {
            var objList = _testRepo.GetTestResults();
            var objDto = new List<TestResultDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TestResultDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual Result
        /// </summary>
        /// <param name="resultId"> The id of the Test Result </param>
        /// <returns></returns>
        [HttpGet("{TestResultId:int}", Name = "GetTestResult")]
        [ProducesResponseType(200, Type = typeof(TestResultDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTestResult(int resultId)
        {
            var obj = _testRepo.GetTestResult(resultId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TestResultDto>(obj);

            return Ok(objDto);

        }

        /// <summary>
        /// Get Test Result 
        /// </summary>
        /// <param name="LabTestid"></param>
        /// <returns></returns>
        [HttpGet("[action]/{LabTestId:int}")]
        [ProducesResponseType(200, Type = typeof(TestResultDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetResultByLabTest(int LabTestid)
        {
            var objList = _testRepo.GetResultByTest(LabTestid);
            if (objList == null)
            {
                return NotFound();
            }
            var objDto = new List<TestResultDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TestResultDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Add New Test Result
        /// </summary>
        /// <param name="labTestsDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TestResultDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTestResult([FromBody] TestResultDto labTestsDto)
        {
            if (labTestsDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_testRepo.ResultExists(labTestsDto.TestName))
            {
                ModelState.AddModelError("", "Result Exists!");
                return StatusCode(404, ModelState);
            }
            var ObjlabTest = _mapper.Map<TestResult>(labTestsDto);
            if (!_testRepo.CreateTestResult(ObjlabTest))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ObjlabTest.TestName}");
                return StatusCode(500, ModelState);
            }
            return new ObjectResult(ObjlabTest) { StatusCode = StatusCodes.Status201Created };
            
        }

        /// <summary>
        /// Update Test Result
        /// </summary>
        /// <param name="TestResultid"></param>
        /// <param name="labTestDto"></param>
        /// <returns></returns>
        [HttpPatch("{TestResultid:int}", Name = "UpdateTestResult")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTestResult(int TestResultid, [FromBody] TestResult labTestDto)
        {
            if (labTestDto == null || TestResultid != labTestDto.LabTestID)
            {
                return BadRequest(ModelState);
            }

            var labTestObj = _mapper.Map<TestResult>(labTestDto);
            if (!_testRepo.UpdateTestResult(labTestObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {labTestObj.TestName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Delete Exisiting Test Result
        /// </summary>
        /// <param name="LabTestID"></param>
        /// <returns></returns>
        [HttpDelete("{LabTestID:int}", Name = "DeleteTestResult")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTestResult(int LabTestID)
        {
            if (!_testRepo.ResultExists(LabTestID))
            {
                return NotFound();
            }

            var labTestObj = _testRepo.GetTestResult(LabTestID);
            if (!_testRepo.DeleteTestResult(labTestObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {labTestObj.TestName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
