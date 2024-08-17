using SlataTestJob.DAL.Models;

namespace SlataTestJob.DAL.Interfaces
{
    public interface ITestJobDAL
    {
        Task<int> CreateTestJobAsync(TestJob testJob);
        Task<TestJob?> GetTestJobAsync(int candidateId);
        Task<TestJob?> UpdateTestJobAsync(TestJob testJob);
        Task<bool> DeleteTestJobAsync(int id);
    }
}
