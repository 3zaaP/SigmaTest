using System.Collections.Generic;
using System.Threading.Tasks;
using JobCandidateHub.Models;

namespace JobCandidateHub.Data
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetCandidateByEmailAsync(string email);
        Task AddOrUpdateCandidateAsync(Candidate candidate);
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task DeleteCandidateAsync(Candidate candidate);
    }
}
