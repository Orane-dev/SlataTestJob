using Microsoft.AspNetCore.Mvc;
using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Models;
using SlataTestJob.DTO;

namespace SlataTestJob.Controllers
{
    [Route("[controller]")]
    public class TestJobController : Controller
    {
        private ITestJobBL _testJobBL;

        public TestJobController(ITestJobBL testJobBL)
        {
            _testJobBL = testJobBL;
        }

        [HttpPost]
        public async Task<ActionResult<TestJobDTO>> CreateTestJob([FromBody]TestJobDTO testJobDTO)
        {
            try
            {
                var job = await _testJobBL.CreateTestJob(testJobDTO);
                return Ok(job);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<TestJobDTO>> GetTestJob(int candidateId)
        {
            try
            {
                var testJob = await _testJobBL.GetTestJob(candidateId);
                if (testJob == null)
                {
                    return NotFound(new {Message = "Test job not found"});
                    //if (Enum.TryParse<JobStatus>(testJob.Status.ToString(), out var status))
                    //{
                    //    testJob.Status = status;
                    //}
                    
                }
                return Ok(testJob);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<TestJobDTO>> UpdateTestJob([FromBody]TestJobDTO testJobDTO)
        {
            try
            {
                var updateTestJob = await _testJobBL.UpdateTestJob(testJobDTO);
                if (updateTestJob == null)
                {
                    return NotFound("Test job not found");
                }
                return NoContent();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTestJob(int id)
        {
            try
            {
                var isDeleted = await _testJobBL.DeleteTestJob(id);
                if (!isDeleted)
                {
                    return NotFound("Test job not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
