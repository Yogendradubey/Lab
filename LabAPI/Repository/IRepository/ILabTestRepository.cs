using System.Collections.Generic;
using LabAPI.Models;

namespace LabAPI.Repository.IRepository
{
    public interface ILabTestRepository
    {
        ICollection<LabTests> GetLabTests();
        ICollection<LabTests> GetLabTestsByPatientId(int pId);
        LabTests GetLabTest(int testId);
        bool LabTestExists(string TestName);
        bool LabTestExists(int id);
        bool CreateLabTest(LabTests tests);
        bool UpdateLabTest(LabTests tests);
        bool DeleteLabTest(LabTests tests);
        bool Save();
    }
}
