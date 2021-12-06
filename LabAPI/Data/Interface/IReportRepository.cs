using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Interface
{
    public interface IReportRepository
    {
        List<Report> GetAllReports();
        Report GetReport(int Id);
        Report AddReport(ReportRequest report);
        string UpdateReport(int id, ReportRequest report);
        string DeleteReport(int Id);

    }
}
