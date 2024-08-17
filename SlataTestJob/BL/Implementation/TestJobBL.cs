using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DTO;

namespace SlataTestJob.BL.Implementation
{
    public class TestJobBL : ITestJobBL
    {
        private ITestJobDAL _testJobDAL;
        private IEmployeeDAL _employeeDAL;
        private ICandidateDAL _candidateDAL;

        public TestJobBL(ITestJobDAL testJobDAL, IEmployeeDAL employeeDAL, ICandidateDAL candidateDAL)
        {
            _candidateDAL = candidateDAL;
            _testJobDAL = testJobDAL;
            _employeeDAL = employeeDAL;
        }

        public async Task<TestJobDTO> CreateTestJob(TestJobDTO testJobDTO)
        {
            var testJob = testJobDTO.ToTestJob();

            var jobId = await _testJobDAL.CreateTestJobAsync(testJob);
            testJobDTO.Id = jobId;

            return testJobDTO;
        }

        public async Task<TestJobDTO?> GetTestJob(int candidateId)
        {
            var testJob = await _testJobDAL.GetTestJobAsync(candidateId);
            if (testJob != null)
            {
                return testJob.ToTestJobDTO();
            }
            return null;
        }

        public async Task<TestJobDTO?> UpdateTestJob(TestJobDTO testJobDTO)
        {
            var testJob = testJobDTO.ToTestJob();
            var updatedTestJob = await _testJobDAL.UpdateTestJobAsync(testJob);

            if (updatedTestJob != null)
            {
                return testJob.ToTestJobDTO();
            }
            return null;
        }

        public async Task<bool> DeleteTestJob(int id)
        {
            return await _testJobDAL.DeleteTestJobAsync(id);
        }
    }
}
