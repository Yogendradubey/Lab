using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _repo;
        public ReportController(IReportRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get All Reports
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Report>> Get()
        {
            List<Report> reports = _repo.GetAllReports();
            return Ok(reports);
        }

        /// <summary>
        /// Get Report By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Report report = _repo.GetReport(id);
            return Ok(report);
        }

        /// <summary>
        /// Create new report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromQuery]ReportRequest report)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid request");
                }

                Report savedReport = _repo.AddReport(report);

                return Ok(savedReport);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            
        }

        /// <summary>
        /// Update existing report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(int id, ReportRequest report)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid request");
                }
                string isUpdated = _repo.UpdateReport(id, report);

                if (isUpdated == "Record not found")
                {
                    return NotFound(isUpdated);
                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            

            return Ok("Updated successfully");
        }

        /// <summary>
        /// Delete report by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string isDeleted = _repo.DeleteReport(id);

            if (isDeleted == "Record not found")
            {
                return NotFound(isDeleted);
            }

            return Ok();
        }
    }
}
