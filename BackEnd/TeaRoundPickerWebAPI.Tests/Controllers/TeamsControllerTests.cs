using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Controllers;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;
using TeaRoundPickerWebAPI.Services.Interfaces;
using Xunit;

namespace TeaRoundPickerWebAPI.Tests.Controllers
{
    public class TeamsControllerTests : TestBase
    {
        private readonly ITeamService _teamService;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _teamService = new TeamService(Context);
            _controller = new TeamsController(_teamService);
        }

        [Fact]
        public async Task GetTeams_ReturnsOkResult()
        {
            // Arrange
            var team = new Team("Test Team");
            await Context.Teams.AddAsync(team);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.GetTeams();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var teams = result.Result.As<OkObjectResult>().Value.Should().BeOfType<List<Team>>().Subject;
            teams.Should().Contain(t => t.Label == "Test Team");
        }

        [Fact]
        public async Task GetTeam_WhenTeamExists_ReturnsOkResult()
        {
            // Arrange
            var team = new Team("Test Team");
            await Context.Teams.AddAsync(team);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.GetTeam(team.Id);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var returnedTeam = result.Result.As<OkObjectResult>().Value.Should().BeOfType<Team>().Subject;
            returnedTeam.Should().BeEquivalentTo(team, options => options
                .Including(t => t.Label));
        }

        [Fact]
        public async Task GetTeam_WhenTeamDoesNotExist_ReturnsInternalServerError()
        {
            // Arrange
            var nonExistentId = 99999;

            // Act
            var result = await _controller.GetTeam(nonExistentId);

            // Assert
            AssertInternalServerError(result.Result);
        }

        [Fact]
        public async Task CreateTeam_WhenValidTeam_ReturnsCreatedResult()
        {
            // Arrange
            var newTeam = new Team("New Team");

            // Act
            var result = await _controller.CreateTeam(newTeam);

            // Assert
            // Verify the team was actually created in the database
            var createdTeam = await Context.Teams.FindAsync(newTeam.Id);
            createdTeam.Should().NotBeNull();
            createdTeam.Should().BeEquivalentTo(newTeam, options => options
                .Including(t => t.Label));

            // Verify CreatedAtActionResult
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result.As<CreatedAtActionResult>();
            createdResult.ActionName.Should().Be("GetTeam");
            createdResult.RouteValues["id"].Should().Be(newTeam.Id);

            var returnedTeam = createdResult.Value.Should().BeOfType<Team>().Subject;
            returnedTeam.Should().BeEquivalentTo(newTeam, options => options
                .Including(t => t.Label));
        }

        [Fact]
        public async Task CreateTeam_WhenInvalidTeam_ReturnsInternalServerError()
        {
            // Arrange
            var invalidTeam = new Team("");

            // Act
            var result = await _controller.CreateTeam(invalidTeam);

            // Assert
            AssertInternalServerError(result.Result);
        }

        [Fact]
        public async Task AddParticipant_WhenValid_ReturnsNoContent()
        {
            // Arrange
            var team = new Team("Test Team");
            var participant = new Participant("John Smith", "Black Tea");
            await Context.Teams.AddAsync(team);
            await Context.Participants.AddAsync(participant);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.AddParticipant(team.Id, participant.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verify the participant was actually added to the team
            var updatedTeam = await Context.Teams
                .Include(t => t.Participants)
                .FirstOrDefaultAsync(t => t.Id == team.Id);
            updatedTeam.Should().NotBeNull();
            updatedTeam.Participants.Should().Contain(p => p.Id == participant.Id);
        }

        [Fact]
        public async Task AddParticipant_WhenInvalid_ReturnsInternalServerError()
        {
            // Arrange
            var nonExistentTeamId = 99999;
            var nonExistentParticipantId = 88888;

            // Act
            var result = await _controller.AddParticipant(nonExistentTeamId, nonExistentParticipantId);

            // Assert
            AssertInternalServerError(result);
        }

        [Fact]
        public async Task RemoveParticipant_WhenValid_ReturnsNoContent()
        {
            // Arrange
            var team = new Team("Test Team");
            var participant = new Participant("John Smith", "Black Tea");
            await Context.Teams.AddAsync(team);
            await Context.Participants.AddAsync(participant);
            team.Participants.Add(participant);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.RemoveParticipant(team.Id, participant.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verify the participant was actually removed from the team
            var updatedTeam = await Context.Teams
                .Include(t => t.Participants)
                .FirstOrDefaultAsync(t => t.Id == team.Id);
            updatedTeam.Should().NotBeNull();
            updatedTeam.Participants.Should().NotContain(p => p.Id == participant.Id);
        }

        [Fact]
        public async Task RemoveParticipant_WhenInvalid_ReturnsInternalServerError()
        {
            // Arrange
            var nonExistentTeamId = 99999;
            var nonExistentParticipantId = 88888;

            // Act
            var result = await _controller.RemoveParticipant(nonExistentTeamId, nonExistentParticipantId);

            // Assert
            AssertInternalServerError(result);
        }
    }
}