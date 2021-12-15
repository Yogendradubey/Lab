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
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _db;
        public ReportController(IReportRepository reportRepository)
        {
            _db = reportRepository;
        }

        /// <summary>
        /// Get All Reports
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Report>> GetReports()
        {
            List<Report> reports = _db.GetAllReports();
            return Ok(reports);
        }

        /// <summary>
        /// Get Report By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetReport(int id)
        {
            Report report = _db.GetReport(id);
            return Ok(report);
        }

        /// <summary>
        /// Create new report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateReport([FromBody] ReportRequest report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _db.AddReport(report);

            return Ok();
        }

        /// <summary>
        /// Update existing report
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateReport(int id, ReportRequest report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Report isReportExist = _db.GetReport(id);
            if (isReportExist == null)
            {
                return NotFound();
            }

            _db.UpdateReport(id, report);


            return Ok();
        }

        /// <summary>
        /// Delete report by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteReport(int id)
        {
            Report isReportExist = _db.GetReport(id);
            if (isReportExist == null)
            {
                return NotFound();
            }
            _db.DeleteReport(id);


            return Ok();
        }
    }
}
