using Microsoft.EntityFrameworkCore;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DAL.Models;
using System.Security.Cryptography.X509Certificates;

namespace SlataTestJob.DAL.Implementation
{
    public class TestJobDAL : ITestJobDAL
    {
        private ApplicationContext _appContext;

        public TestJobDAL(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<int> CreateTestJobAsync(TestJob testJob)
        {
            await _appContext.TestJobs.AddAsync(testJob);
            await _appContext.SaveChangesAsync();

            return testJob.Id;
        }

        public async Task<TestJob?> GetTestJobAsync(int candidateId)
        {
            var testJob = await _appContext.TestJobs.FirstOrDefaultAsync(x => x.CandidateId == candidateId);
            return testJob;
        }

        public async Task<TestJob?> UpdateTestJobAsync(TestJob testJob)
        {
            var jobToUpdate = await _appContext.TestJobs.FirstOrDefaultAsync(x => x.Id == testJob.Id);
            if (jobToUpdate != null)
            {
                jobToUpdate.StartDate = testJob.StartDate;
                jobToUpdate.EndDate = testJob.EndDate;
                jobToUpdate.ReceiveDate = testJob.ReceiveDate;
                jobToUpdate.Status = testJob.Status;
                jobToUpdate.Description = testJob.Description;

                await _appContext.SaveChangesAsync();
            }
       
            return jobToUpdate;
        }

        public async Task<bool> DeleteTestJobAsync(int id)
        {
            var deleteJob = await _appContext.TestJobs.FirstOrDefaultAsync(x => id == x.Id);

            if (deleteJob != null)
            {
                _appContext.TestJobs.Remove(deleteJob);
                await _appContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
