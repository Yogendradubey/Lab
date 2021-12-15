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
        void AddReport(ReportRequest report);
        void UpdateReport(int id, ReportRequest report);
        void DeleteReport(int Id);

    }
}
