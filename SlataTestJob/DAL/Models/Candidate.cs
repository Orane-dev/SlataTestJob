using SlataTestJob.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlataTestJob.DAL.Models
{
    public class Candidate
    { 
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobPosition { get; set; }
        public string Department { get; set; }
        public DateOnly? InterviewDate { get; set; }
        public int InterviewManagerId { get; set; }
        [ForeignKey(nameof(InterviewManagerId))]
        public Employee InterviewManager { get; set; }

        public CandidateDTO ToCandidateDTO()
        {
            return new CandidateDTO
            {
                Id = Id,
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                JobPosition = JobPosition,
                Department = Department,
                IterviewDate = InterviewDate,
                InterviewManager = InterviewManager.Fullname,
            };
        }
    }
}
