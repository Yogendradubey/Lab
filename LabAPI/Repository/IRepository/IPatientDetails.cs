using LabAPI.Models;
using System.Collections.Generic;

namespace LabAPI.Repository.IRepository
{
    public interface IPatientDetails
    {
        ICollection<PatientDetails> GetPatientDetails();
        PatientDetails GetPatientDetail(int PatientID);
        bool PatientByName(string PatientName);
        bool PatientByID(int PatientID);
        bool CreatePatientDetail(PatientDetails patientDetail);
        bool UpdatePatientDetails(PatientDetails patientDetail);
        bool DeletePatientDetail(PatientDetails patientDetail);
        bool Save();
        bool PatientExists(int id);
    }
}
