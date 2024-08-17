using Microsoft.EntityFrameworkCore;
using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DAL.Models;
using SlataTestJob.DTO;

namespace SlataTestJob.DAL.Implementation
{
    public class CandidateDAL : ICandidateDAL
    {
        private ApplicationContext _appContext;

        public CandidateDAL(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync()
        {
            var candidates = await _appContext.Candidates.Include(e => e.InterviewManager).ToListAsync();
            return candidates;
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesAsync(string department)
        {
            var candidates = await _appContext.Candidates.Include(e => e.InterviewManager).Where(x => x.Department == department).ToListAsync();
            return candidates;
        }
        public async Task<Candidate?> GetCandidateAsync(string fullName)
        {
            var candidate = await _appContext.Candidates.FirstOrDefaultAsync(x => x.FullName == fullName);
            return candidate;
        }

        public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
        {
            await _appContext.Candidates.AddAsync(candidate);
            await _appContext.SaveChangesAsync();
            return candidate;
        }

        public async Task<bool> DeleteCandidateByIdAsync(int id)
        {
            var candidate = await _appContext.Candidates.FirstOrDefaultAsync(x => x.Id == id);
            if (candidate != null)
            {
                _appContext.Candidates.Remove(candidate);
                await _appContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
