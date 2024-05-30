using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobCandidateHub.Controllers;
using JobCandidateHub.Data;
using JobCandidateHub.Models;
using System.Collections.Generic;

namespace JobCandidateHub.Tests
{
    public class CandidatesControllerTests
    {
        private readonly Mock<ICandidateRepository> _mockRepo;
        private readonly CandidatesController _controller;

        public CandidatesControllerTests()
        {
            _mockRepo = new Mock<ICandidateRepository>();
            _controller = new CandidatesController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnOkResult()
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
            var result = await _controller.AddOrUpdateCandidate(candidate);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetCandidateByEmail_ShouldReturnCandidate()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            _mockRepo.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email))
                .ReturnsAsync(candidate);

            // Act
            var result = await _controller.GetCandidateByEmail(candidate.Email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(candidate.Email, returnValue.Email);
        }

        [Fact]
        public async Task GetAllCandidates_ShouldReturnAllCandidates()
        {
            // Arrange
            var candidates = new List<Candidate>
            {
                new Candidate { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Comment = "Test Comment" },
                new Candidate { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Comment = "Test Comment" }
            };

            _mockRepo.Setup(repo => repo.GetAllCandidatesAsync())
                .ReturnsAsync(candidates);

            // Act
            var result = await _controller.GetAllCandidates();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Candidate>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task DeleteCandidate_ShouldReturnOkResult()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test Comment"
            };

            _mockRepo.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email))
                .ReturnsAsync(candidate);

            // Act
            var result = await _controller.DeleteCandidate(candidate.Email);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
