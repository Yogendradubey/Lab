using System.Collections.Generic;
using System.Linq;
using LabAPI.Data;
using LabAPI.Models;
using LabAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LabAPI.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApplicationDbContext _db;

        public TestResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTestResult(TestResult testResult)
        {
            _db.Results.Add(testResult);
            return Save();
        }

        public bool DeleteTestResult(TestResult testResult)
        {
            _db.Results.Remove(testResult);
            return Save();
        }

        public ICollection<TestResult> GetResultByTest(int testId)
        {
            return _db.Results.Include(c => c.LabTests).Where(c => c.LabTestID == testId).ToList();
        }

        public TestResult GetTestResult(int testId)
        {
            return _db.Results.Include(c => c.LabTests).FirstOrDefault(a => a.TestResultID == testId);

        }

        public ICollection<TestResult> GetTestResults()
        {
            return _db.Results.Include(c => c.LabTests).OrderBy(a => a.TestName).ToList();

        }

        public bool ResultExists(string TestName)
        {
            bool value = _db.Results.Any(a => a.TestName.ToLower().Trim() == TestName.ToLower().Trim());
            return value;
        }

        public bool ResultExists(int id)
        {
            return _db.Results.Any(a => a.TestResultID == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTestResult(TestResult testResult)
        {
            _db.Results.Update(testResult);
            return Save();
        }
    }
}
