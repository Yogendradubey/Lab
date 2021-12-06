using LabDemo.Models;

namespace LabDemo.Services.LabReports
{
    public interface ILabReportService
    {
        List<LabReport> GetLabReports();
        LabReport GetLabReport(int id);
        List<LabReport> GetLabReport(DateTime startDate, DateTime endDate);
        void AddLabReport(LabReport report);
        void UpdateLabReport(LabReport report);
        void DeleteLabReport(int id);

    }
}
