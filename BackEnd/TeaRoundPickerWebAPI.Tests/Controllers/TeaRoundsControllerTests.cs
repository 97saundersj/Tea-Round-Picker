using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Controllers;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;
using TeaRoundPickerWebAPI.Services.Interfaces;
using Xunit;

namespace TeaRoundPickerWebAPI.Tests.Controllers
{
    public class TeaRoundsControllerTests : TestBase
    {
        private readonly ITeaRoundService _teaRoundService;
        private readonly TeaRoundsController _controller;

        public TeaRoundsControllerTests()
        {
            _teaRoundService = new TeaRoundService(Context);
            _controller = new TeaRoundsController(_teaRoundService);
        }

        [Fact]
        public async Task GetTeaRounds_WhenTeamExists_ReturnsOkResult()
        {
            // Arrange
            var team = new Team("Test Team");
            await Context.Teams.AddAsync(team);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.GetTeaRounds(team.Id);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var teaRounds = result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<TeaRound>>().Subject;
            teaRounds.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTeaRounds_WhenTeamDoesNotExist_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var nonExistentTeamId = 99999;

            // Act
            var result = await _controller.GetTeaRounds(nonExistentTeamId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var teaRounds = result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<TeaRound>>().Subject;
            teaRounds.Should().BeEmpty();
        }

        [Fact]
        public async Task AddTeaRound_WhenTeamExists_ReturnsOkResult()
        {
            // Arrange
            var team = new Team("Test Team");
            var participant = new Participant("John Smith", "Black Tea");
            await Context.Teams.AddAsync(team);
            await Context.Participants.AddAsync(participant);
            team.Participants.Add(participant);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.AddTeaRound(team.Id);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var selectedParticipantId = result.Result.As<OkObjectResult>().Value.Should().BeOfType<int>().Subject;
            selectedParticipantId.Should().NotBe(null);
        }

        [Fact]
        public async Task AddTeaRound_WhenTeamDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var nonExistentTeamId = 99999;

            // Act
            var result = await _controller.AddTeaRound(nonExistentTeamId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AddTeaRound_WhenTeamHasNoParticipants_ReturnsNotFound()
        {
            // Arrange
            var team = new Team("Test Team");
            await Context.Teams.AddAsync(team);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.AddTeaRound(team.Id);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
} 