using ShushrutEyeHospitalCRM.Models;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IReceptionService
    {
        Task<string> InsertReception(ReceptionistViewModel receptionistViewModel);
        Task<string> DeleteReception(int Id);
        Task<string> UpdateReception(ReceptionistViewModel receptionistViewModel);
        Task<IEnumerable<ReceptionistViewModel>> GetReceptionist();
        Task<ReceptionistViewModel> GetReceptionistById(int Id);
        Task<IEnumerable<ReceptionistViewModel>> GetActiveReceptionist();
    }
}
