using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Repository
{
    public class ReportRepository: IReportRepository
    {
        private readonly LaboratoryDbContext _dbContext;

        public ReportRepository(LaboratoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Report
        public Report AddReport(ReportRequest report)
        {
            try
            {
                Random id = new Random();
                using (var db = _dbContext)
                {
                    Report _report = new Report();
                    _report.Id = id.Next(100);
                    _report.Type = report.Type.ToString();
                    _report.SampleCollectionDateTime = report.SampleCollectionDateTime;
                    _report.Result = report.Result.ToString();
                    _report.CreatedOn = DateTime.Now;
                    _report.PatientId = report.PatientId;
                    db.Report.Add(_report);
                    db.SaveChanges();
                    return _report;
                }
            }
            catch
            {
                throw;
            }
        }
        public string DeleteReport(int Id)
        {
            var record = _dbContext.Report.Where(x => x.Id == Id).FirstOrDefault();

            if (record != null)
            {
                _dbContext.Entry(record).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return ResponseMessage.DelectedSuccessfully.ToString();
            }
            else
            {
                return ResponseMessage.RecordNotFound.ToString();
            }

        }
        public List<Report> GetAllReports()
        {
            return _dbContext.Report.ToList();
        }
        public Report GetReport(int Id)
        {
            return _dbContext.Report.FirstOrDefault(x => x.Id == Id);
        }
        public string UpdateReport(int id, ReportRequest report)
        {
            try
            {
                using (var db = _dbContext)
                {
                    var existingData = db.Report.Where(x => x.Id == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        existingData.Result = report.Result.ToString();
                        existingData.SampleCollectionDateTime = report.SampleCollectionDateTime;
                        existingData.Type = report.Type.ToString();
                        existingData.CreatedOn = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        return ResponseMessage.RecordNotFound.ToString();
                    }
                }

            }
            catch
            {
                throw;
            }
            return ResponseMessage.UpdatedSuccessfully.ToString();
        }
        #endregion

    }
}
