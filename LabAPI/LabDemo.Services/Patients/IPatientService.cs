using LabDemo.Models;

namespace LabDemo.Services.Patients
{
    public interface IPatientService
    {
        List<Patient> GetPatients();
        Patient GetPatient(int id);
        List<Patient> GetPatients(TestTypeEnum type, DateTime startDate, DateTime endDate);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);
    }
}
