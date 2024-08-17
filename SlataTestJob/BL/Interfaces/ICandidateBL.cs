using SlataTestJob.DAL.Models;
using SlataTestJob.DTO;

namespace SlataTestJob.BL.Interfaces
{
    public interface ICandidateBL
    {
        Task<IEnumerable<CandidateDTO>> GetCandidates();
        Task<IEnumerable<CandidateDTO>> GetCandidates(string department);
        Task<CandidateDTO?> CreateCandidate(CandidateDTO candidateDTO);
        Task<bool> DeleteCandidateById(int id);
    }
}
