using SlataTestJob.DAL.Models;

namespace SlataTestJob.DTO
{
    public class TestJobDTO
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly? ReciveDate {  get; set; }
        public JobStatus Status { get; set; }
        public string? Description { get; set; }

        public TestJob ToTestJob()
        {
            return new TestJob
            {
                Id = Id,
                CandidateId = CandidateId,
                StartDate = StartDate,
                EndDate = EndDate,
                ReceiveDate = ReciveDate,
                Status = Status,
                Description = Description
            };
        }
    }
}
