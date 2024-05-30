using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobCandidateHub.Data;
using JobCandidateHub.Models;

namespace JobCandidateHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidatesController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateRepository.AddOrUpdateCandidateAsync(candidate);
            return Ok();
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCandidateByEmail(string email)
        {
            var candidate = await _candidateRepository.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _candidateRepository.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteCandidate(string email)
        {
            var candidate = await _candidateRepository.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound();
            }

            await _candidateRepository.DeleteCandidateAsync(candidate);
            return Ok();
        }
    }
}
