using AutoMapper;
using LabDemo.DataProvider;

namespace LabDemo.Services.LabReports
{
    public class LabReportService:ILabReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LabReportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddLabReport(Models.LabReport report)
        {
            var labReport = _mapper.Map<DataProvider.LabReport>(report);
            _context.LabReports.Add(labReport);
            _context.SaveChanges();
        }

        public void DeleteLabReport(int id)
        {
            var report =_context.LabReports.Find(id);
            if (report!=null)
            {
                _context.LabReports.Remove(report);
                _context.SaveChanges();
            }
        }

        public Models.LabReport GetLabReport(int id)
        {
            var report = _context.LabReports.Find(id);
            if (report!=null)
            {
                return _mapper.Map<Models.LabReport>(report);
            }
            return null;
        }

        public List<Models.LabReport> GetLabReport(DateTime startDate, DateTime endDate)
        {
            return _mapper.Map<List<Models.LabReport>>(_context.LabReports.ToList()) ;
        }

        public List<Models.LabReport> GetLabReports()
        {
           return _mapper.Map<List<Models.LabReport>>(_context.LabReports.ToList());
        }

        public void UpdateLabReport(Models.LabReport report)
        {
            var labReport = _mapper.Map<DataProvider.LabReport>(report);
            _context.LabReports.Update(labReport);
            _context.SaveChanges();
        }
    }
}
