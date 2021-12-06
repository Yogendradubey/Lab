using AutoMapper;
using LabDemo.DataProvider;

namespace LabDemo.Services.Patients
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PatientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddPatient(Models.Patient patient)
        {
            var patientEntity = _mapper.Map<Patient>(patient);
            _context.Patients.Add(patientEntity);
            _context.SaveChanges();
        }

        public void DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        public Models.Patient GetPatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                return _mapper.Map<Models.Patient>(patient);
            }
            return null ;
        }

        public List<Models.Patient> GetPatients(Models.TestTypeEnum type, DateTime startDate, DateTime endDate)
        {
            var patients=(from p in  _context.Patients
                          join r in _context.LabReports on p.PId equals r.PId
                          where r.Type.ToString() == type.ToString() 
                          &&  r.TestTime.Date >= startDate.Date && r.TestTime.Date <= endDate.Date
                          select  p).ToList();

            return _mapper.Map<List<Models.Patient>>(patients);
        }

        public List<Models.Patient> GetPatients()
        {
            return _mapper.Map<List<Models.Patient>>(_context.Patients.ToList());
        }

        public void UpdatePatient(Models.Patient patient)
        {
            var patientEntity = _mapper.Map<Patient>(patient);
            _context.Patients.Update(patientEntity);
            _context.SaveChanges();
        }
    }
}
