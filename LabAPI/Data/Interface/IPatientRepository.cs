using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Interface
{
    public interface IPatientRepository
    {
        List<Patient> GetAllPatients();
        Patient GetPatient(int Id);
        Patient AddPatient(PatientRequest patient);
        string UpdatePatient(int id, PatientRequest patient);
        string DeletePatient(int Id);
        List<Patient> GetPatientsByFilter(ReportFilterRequest filter);
    }
}
