using SlataTestJob.DAL.Models;

namespace SlataTestJob.DAL.Interfaces
{
    public interface ICandidateDAL
    {
        Task<IEnumerable<Candidate>> GetCandidatesAsync();
        Task<IEnumerable<Candidate>> GetCandidatesAsync(string department);
        Task<Candidate> CreateCandidateAsync(Candidate candidate);
        Task<Candidate?> GetCandidateAsync(string fullName);
        Task<bool> DeleteCandidateByIdAsync(int id);
    }
}
