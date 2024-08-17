using SlataTestJob.DAL.Models;

namespace SlataTestJob.DTO
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobPosition { get; set; }
        public string Department { get; set; }
        public DateOnly? IterviewDate { get; set; }
        public string InterviewManager { get; set; }
        public string InterviewHR { get; set; }

        public Candidate ToCandidate()
        {
            return new Candidate
            {
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                JobPosition = JobPosition,
                Department = Department,
                InterviewDate = IterviewDate,
            };
        }
    }
}
