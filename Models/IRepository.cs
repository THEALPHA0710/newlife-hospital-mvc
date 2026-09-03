namespace NewLifeHospital.Models
{
    public interface IRepository
    {
        bool RegisterForMembership(PatientInfoDetail pObj);

        bool CancelMembership(int registrationId);

        bool UpdateEmail(int registrationId, string email);
    }
}
