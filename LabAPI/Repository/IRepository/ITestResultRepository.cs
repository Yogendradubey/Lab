using System.Collections.Generic;
using LabAPI.Models;

namespace LabAPI.Repository.IRepository
{
    public interface ITestResultRepository
    {

        ICollection<TestResult> GetTestResults();
        ICollection<TestResult> GetResultByTest(int testId);
        TestResult GetTestResult(int testId);
        bool ResultExists(string TestName);
        bool ResultExists(int id);
        bool CreateTestResult(TestResult testResult);
        bool UpdateTestResult(TestResult testResult);
        bool DeleteTestResult(TestResult testResult);
        bool Save();
    }
}
