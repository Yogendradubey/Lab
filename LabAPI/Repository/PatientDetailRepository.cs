using System.Collections.Generic;
using System.Linq;
using LabAPI.Data;
using LabAPI.Models;
using LabAPI.Repository.IRepository;

namespace LabAPI.Repository
{
    public class PatientDetailRepository : IPatientDetails
    {
        private readonly ApplicationDbContext _db;

        public PatientDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreatePatientDetail(PatientDetails patientDetail)
        {
            _db.PatientDetails.Add(patientDetail);
            return Save();
        }

        public bool DeletePatientDetail(PatientDetails patientDetail)
        {
            _db.PatientDetails.Remove(patientDetail);
            return Save();
        }

        public PatientDetails GetPatientDetail(int PatientID)
        {
            return _db.PatientDetails.FirstOrDefault(a => a.PatientID == PatientID);
        }

        public ICollection<PatientDetails> GetPatientDetails()
        {
            return _db.PatientDetails.OrderBy(a => a.PatientName).ToList();
        }

        public bool PatientByID(int PatientID)
        {
            bool value = _db.PatientDetails.Any(a => a.PatientID == PatientID);
            return value;
        }

        public bool PatientByName(string PatientName)
        {
            bool value = _db.PatientDetails.Any(a => a.PatientName.ToLower().Trim() == PatientName.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePatientDetails(PatientDetails patientDetail)
        {
            _db.PatientDetails.Update(patientDetail);
            return Save();
        }

        public bool PatientExists(int PatientID)
        {
            return _db.PatientDetails.Any(a => a.PatientID == PatientID);
        }
    }
}

