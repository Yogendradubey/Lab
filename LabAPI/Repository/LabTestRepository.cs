using LabAPI.Data;
using LabAPI.Models;
using LabAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabAPI.Repository
{
    public class LabTestRepository : ILabTestRepository
    {
        private readonly ApplicationDbContext _db;

        public LabTestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ICollection<LabTests> GetLabTests()
        {
            return _db.LabTests.Include(c => c.Patients).OrderBy(a => a.LabTestName).ToList();
        }

        public ICollection<LabTests> GetLabTestsByPatientId(int pId)
        {
            return _db.LabTests.Include(c => c.Patients).Where(c => c.PatientID == pId).ToList();
        }

        public LabTests GetLabTest(int testId)
        {
            return _db.LabTests.Include(c => c.Patients).FirstOrDefault(a => a.LabTestID == testId);

        }

        public bool LabTestExists(string TestName)
        {
            bool value = _db.LabTests.Any(a => a.LabTestName.ToLower().Trim() == TestName.ToLower().Trim());
            return value;
        }

        public bool LabTestExists(int id)
        {
            return _db.LabTests.Any(a => a.LabTestID == id);
        }

        public bool CreateLabTest(LabTests labTests)
        {
            _db.LabTests.Add(labTests);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateLabTest(LabTests labtest)
        {
            _db.LabTests.Update(labtest);
            return Save();
        }

        public bool DeleteLabTest(LabTests labtest)
        {
            _db.LabTests.Remove(labtest);
            return Save();
        }
    }
}
