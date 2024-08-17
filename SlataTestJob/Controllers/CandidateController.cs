using Microsoft.AspNetCore.Mvc;
using SlataTestJob.BL.Implementation;
using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Implementation;
using SlataTestJob.DAL.Models;
using SlataTestJob.DTO;

namespace SlataTestJob.Controllers
{
    [Route("[controller]")]
    public class CandidateController : Controller
    {
        private ICandidateBL _candidateBL;

        public CandidateController(ICandidateBL candidateBL)
        {
            _candidateBL = candidateBL;
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateCandidate([FromBody]CandidateDTO candidateDTO)
        {
            try
            {
                var candidate = await _candidateBL.CreateCandidate(candidateDTO);
                if (candidate == null)
                {
                    return BadRequest("Incorrect data");
                }
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            try
            {
                var candidates = await _candidateBL.GetCandidates();
                if (candidates.Any())
                {
                    return Ok(candidates);
                }
                return NotFound(new { Message = "Users not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" +  ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetCandidatesByDepartment")]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidatesByDepartment(string department)
        {
            try
            {
                var candidates = await _candidateBL.GetCandidates(department);
                if (candidates.Any())
                {
                    return Ok(candidates);
                }
                return NotFound(new { Message = "Users not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCandidateById(int id)
        {
            try
            {
                var isDeleted = await _candidateBL.DeleteCandidateById(id);
                if (!isDeleted)
                {
                    return NotFound(new { Message = "Candidate not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }
    }
}
