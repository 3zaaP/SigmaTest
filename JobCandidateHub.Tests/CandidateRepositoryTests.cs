using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using JobCandidateHub.Data;
using JobCandidateHub.Models;
using System.Collections.Generic;

namespace JobCandidateHub.Tests
{
    public class CandidateRepositoryTests
    {
        private readonly CandidateRepository _repository;
        private readonly ApplicationDbContext _context;

        public CandidateRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new CandidateRepository(_context);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldAddCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            // Act
            await _repository.AddOrUpdateCandidateAsync(candidate);

            // Assert
            var result = await _repository.GetCandidateByEmailAsync(candidate.Email);
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldUpdateCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            await _repository.AddOrUpdateCandidateAsync(candidate);

            // Act
            candidate.FirstName = "Jane";
            await _repository.AddOrUpdateCandidateAsync(candidate);

            // Assert
            var result = await _repository.GetCandidateByEmailAsync(candidate.Email);
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);
        }

        [Fact]
        public async Task GetAllCandidatesAsync_ShouldReturnAllCandidates()
        {
            // Arrange
            var candidate1 = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            var candidate2 = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Comment = "Test Comment"
            };

            await _repository.AddOrUpdateCandidateAsync(candidate1);
            await _repository.AddOrUpdateCandidateAsync(candidate2);

            // Act
            var result = await _repository.GetAllCandidatesAsync();

            // Assert
            Assert.Equal(2, ((List<Candidate>)result).Count);
        }

        [Fact]
        public async Task DeleteCandidateAsync_ShouldRemoveCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            await _repository.AddOrUpdateCandidateAsync(candidate);

            // Act
            var candidateToDelete = await _repository.GetCandidateByEmailAsync(candidate.Email);
            await _repository.DeleteCandidateAsync(candidateToDelete);

            // Assert
            var result = await _repository.GetCandidateByEmailAsync(candidate.Email);
            Assert.Null(result);
        }
    }
}
