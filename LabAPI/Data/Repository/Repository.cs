using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using LaboratoryAPI.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Repository
{
    public class Repository : IPatientRepository, IReportRepository, IUserRepository
    {
        private readonly LaboratoryDbContext _dbContext;
        private readonly string _key;

        public Repository(LaboratoryDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _key = config.GetValue<string>("Key");
        }

        #region Patient
        public Patient AddPatient(PatientRequest patient)
        {
            try
            {
                Random id = new Random();
                using (var x = _dbContext)
                {
                    Patient p = new Patient();
                    p.Id = id.Next(100);
                    p.Name = patient.Name;
                    p.DOB = patient.DOB;
                    p.Gender = patient.Gender.ToString();
                    p.CretedOn = DateTime.Now;
                    x.Patient.Add(p);
                    x.SaveChanges();
                    return p;
                }
            }
            catch
            {
                throw;
            }
        }
        public string DeletePatient(int Id)
        {
            var x = _dbContext.Patient.Where(x => x.Id == Id).FirstOrDefault();
            if (x != null)
            {
                _dbContext.Entry(x).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return "Deleted successfully";
            }
            else
            {
                return "Record not found";
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
            try
            {
                patientsList = (from r in _dbContext.Report
                                              where r.Type.ToString() == filter.Type.ToString() && (r.CreatedOn.Date >= filter.CreatedOnStart.Date && r.CreatedOn.Date <= filter.CreatedOnEnd.Date)
                                              join p in _dbContext.Patient on r.PatientId equals p.Id
                                              select p).ToList<Patient>();
            }
            catch
            {
                throw;
            }           

            return patientsList;
        }
        public string UpdatePatient(int id, PatientRequest patient)
        {
            try
            {
                using (var x = _dbContext)
                {
                    var existingData = x.Patient.Where(x => x.Id == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        existingData.Name = patient.Name;
                        existingData.DOB = patient.DOB;
                        existingData.Gender = patient.Gender.ToString();
                        existingData.CretedOn = DateTime.Now;
                        x.SaveChanges();
                    }
                    else
                    {
                        return "Record not found";
                    }
                }
            }
            catch
            {
                throw;
            }
            return "Updated Successfuly";
        }

        #endregion

        #region Report
        public Report AddReport(ReportRequest report)
        {
            try
            {
                Random id = new Random();
                using (var x = _dbContext)
                {
                    Report r = new Report();
                    r.Id = id.Next(100);
                    r.Type = report.Type.ToString();
                    r.SampleCollectionDateTime = report.SampleCollectionDateTime;
                    r.Result = report.Result.ToString();
                    r.CreatedOn = DateTime.Now;
                    r.PatientId = report.PatientId;
                    x.Report.Add(r);
                    x.SaveChanges();
                    return r;
                }
            }
            catch
            {
                throw;
            }
        }
        public string DeleteReport(int Id)
        {
            var x = _dbContext.Report.Where(x => x.Id == Id).FirstOrDefault();

            if (x != null)
            {
                _dbContext.Entry(x).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return "Deleted successfully";
            }
            else
            {
                return "Record not found";
            }

        }
        public List<Report> GetAllReports()
        {
            return _dbContext.Report.ToList();
        }
        public Report GetReport(int Id)
        {
            return _dbContext.Report.FirstOrDefault(x => x.Id == Id);
        }
        public string UpdateReport(int id, ReportRequest report)
        {
            try
            {
                using (var x = _dbContext)
                {
                    var existingData = x.Report.Where(x => x.Id == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        existingData.Result = report.Result.ToString();
                        existingData.SampleCollectionDateTime = report.SampleCollectionDateTime;
                        existingData.Type = report.Type.ToString();
                        existingData.CreatedOn = DateTime.Now;
                        x.SaveChanges();
                    }
                    else
                    {
                        return "Record not found";
                    }
                }

            }
            catch
            {
                throw;
            }
            return "Updated Successfuly";
        }
        #endregion

        #region User
        public string AuthenticateUser(string username, string password)
        {
            var user = _dbContext.UserCredential.FirstOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateUser(string username, string password)
        {
            try
            {
                Random id = new Random();
                using (var x = _dbContext)
                {
                    UserCredential u = new UserCredential();
                    u.Id = id.Next(100);
                    u.UserName = username;
                    u.Password = password;
                    x.UserCredential.Add(u);
                    x.SaveChanges();
                    return "Created successfully";
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
