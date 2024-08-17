using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DTO;

namespace SlataTestJob.BL.Implementation
{
    public class CandidateBL : ICandidateBL
    {
        private ICandidateDAL _candidateDAL { get; set; }
        private IEmployeeDAL _employeeDAL { get; set; }

        public CandidateBL(ICandidateDAL candidateDAL, IEmployeeDAL employeeDAL)
        {
            _candidateDAL = candidateDAL;
            _employeeDAL = employeeDAL;
        }

        public async Task<IEnumerable<CandidateDTO>> GetCandidates()
        {
            var candidates = await _candidateDAL.GetCandidatesAsync();
            var resultDTO = new List<CandidateDTO>();
            if (candidates.Any())
            {
                foreach (var candidate in candidates)
                {
                    resultDTO.Add(candidate.ToCandidateDTO());
                }
            }
            return resultDTO;
        }

        public async Task<IEnumerable<CandidateDTO>> GetCandidates(string department)
        {
            var candidates = await _candidateDAL.GetCandidatesAsync(department);
            var resultDTO = new List<CandidateDTO>();
            if (candidates != null)
            {
                foreach (var candidate in candidates)
                {
                    resultDTO.Add(candidate.ToCandidateDTO());
                }
            }
            return resultDTO;
        }

        public async Task<CandidateDTO?> CreateCandidate(CandidateDTO candidateDTO)
        {

            var interviewManager = await _employeeDAL.GetEmployeeAsync(candidateDTO.InterviewManager);
            if (interviewManager != null)
            {
                var candidate = candidateDTO.ToCandidate();
                candidate.InterviewManager = interviewManager;

                var createdCandidate = await _candidateDAL.CreateCandidateAsync(candidate);
                return createdCandidate.ToCandidateDTO();
            }
            return null;
        }
        
        public async Task<bool> DeleteCandidateById(int id)
        {
            return await _candidateDAL.DeleteCandidateByIdAsync(id);
        }
    }
}
