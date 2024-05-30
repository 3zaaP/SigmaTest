using Microsoft.EntityFrameworkCore;
using JobCandidateHub.Models;

namespace JobCandidateHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
