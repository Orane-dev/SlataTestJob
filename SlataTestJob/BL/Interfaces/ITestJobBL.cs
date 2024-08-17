using SlataTestJob.DTO;

namespace SlataTestJob.BL.Interfaces
{
    public interface ITestJobBL
    {
        Task<TestJobDTO> CreateTestJob(TestJobDTO testJobDTO);
        Task<TestJobDTO?> GetTestJob(int candidateId);
        Task<TestJobDTO?> UpdateTestJob(TestJobDTO testJobDTO);
        Task<bool> DeleteTestJob(int id);
    }
}
