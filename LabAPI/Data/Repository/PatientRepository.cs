using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using LaboratoryAPI.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly LaboratoryDbContext _dbContext;

        public PatientRepository(LaboratoryDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
        }

        #region Patient
        public void AddPatient(PatientRequest patient)
        {            
                Random id = new Random();
                using (var db = _dbContext)
                {
                    Patient _patient = new Patient();
                    _patient.Id = id.Next(100);
                    _patient.Name = patient.Name;
                    _patient.DOB = patient.DOB;
                    _patient.Gender = patient.Gender.ToString();
                    _patient.CretedOn = DateTime.Now;
                    db.Patient.Add(_patient);
                    db.SaveChanges();
                }
           
        }
        public void DeletePatient(int Id)
        {
            var _record = _dbContext.Patient.Where(x => x.Id == Id).FirstOrDefault();
            if (_record != null)
            {
                _dbContext.Entry(_record).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(_record.Id.ToString());
            }
        }
        public List<Patient> GetAllPatients()
        {
            return _dbContext.Patient.ToList();
        }
        public Patient GetPatient(int Id)
        {
            return _dbContext.Patient.FirstOrDefault(x => x.Id == Id);
        }
        public List<Patient> GetPatientsByFilter(ReportFilterRequest filter)
        {
            List<Patient> patientsList = null;
           
                patientsList = (from r in _dbContext.Report
                                              where r.Type.ToString() == filter.Type.ToString() && (r.CreatedOn.Date >= filter.CreatedOnStart.Date && r.CreatedOn.Date <= filter.CreatedOnEnd.Date)
                                              join p in _dbContext.Patient on r.PatientId equals p.Id
                                              select p).ToList<Patient>();
          
            return patientsList;
        }
        public void UpdatePatient(int id, PatientRequest patient)
        {
         
                using (var db = _dbContext)
                {
                    var existingData = db.Patient.Where(x => x.Id == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        existingData.Name = patient.Name;
                        existingData.DOB = patient.DOB;
                        existingData.Gender = patient.Gender.ToString();
                        existingData.CretedOn = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                    throw new ArgumentNullException(existingData.Id.ToString());
                    }
                }          
        }

        #endregion

    }
}
