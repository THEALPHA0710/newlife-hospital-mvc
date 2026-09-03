using System;
using System.Linq;

namespace NewLifeHospital.Models
{
    public class Repository : IRepository
    {
        private readonly PatientInfoDbContext _context;

        public Repository(PatientInfoDbContext context)
        {
            _context = context;
        }

        public bool RegisterForMembership(PatientInfoDetail pObj)
        {
            try
            {
                if (pObj == null)
                {
                    return false;
                }

                _context.PatientInfoDetail.Add(pObj);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CancelMembership(int registrationId)
        {
            try
            {
                PatientInfoDetail patient = _context.PatientInfoDetail
                    .FirstOrDefault(p => p.RegistrationID == registrationId);

                if (patient == null)
                {
                    return false;
                }

                _context.PatientInfoDetail.Remove(patient);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateEmail(int registrationId, string email)
        {
            try
            {
                PatientInfoDetail patient = _context.PatientInfoDetail
                    .FirstOrDefault(p => p.RegistrationID == registrationId);

                if (patient == null)
                {
                    return false;
                }

                patient.EmailID = email;
                _context.PatientInfoDetail.Update(patient);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
