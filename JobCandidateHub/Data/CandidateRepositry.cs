using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobCandidateHub.Models;

namespace JobCandidateHub.Data
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await GetCandidateByEmailAsync(candidate.Email);
            if (existingCandidate == null)
            {
                _context.Candidates.Add(candidate);
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task DeleteCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
