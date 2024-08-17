using SlataTestJob.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlataTestJob.DAL.Models
{
    public class TestJob
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        [ForeignKey(nameof(CandidateId))]
        public Candidate? Candidate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly? ReceiveDate {  get; set; }
        public JobStatus Status { get; set; }
        public string? Description { get; set; }

        public TestJobDTO ToTestJobDTO()
        {
            return new TestJobDTO
            {
                Id = Id,
                CandidateId = CandidateId,
                StartDate = StartDate,
                EndDate = EndDate,
                ReciveDate = ReceiveDate,
                Status = Status,
                Description = Description,
            };
        }
    }

    [Flags]
    public enum JobStatus
    {
        None = 0,
        InProgress = 1,
        Postponed = 2,
        Done = 4,
    }
}
