using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Controllers;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;
using TeaRoundPickerWebAPI.Services.Interfaces;
using Xunit;

namespace TeaRoundPickerWebAPI.Tests.Controllers
{
    public class ParticipantControllerTests : TestBase
    {
        private readonly IParticipantService _participantService;
        private readonly ParticipantController _controller;

        public ParticipantControllerTests()
        {
            _participantService = new ParticipantService(Context);
            _controller = new ParticipantController(_participantService);
        }

        [Fact]
        public async Task GetParticipant_WhenParticipantExists_ReturnsOkResult()
        {
            // Arrange
            var participant = new Participant("John Smith", "Black Tea");
            await Context.Participants.AddAsync(participant);
            await Context.SaveChangesAsync();

            // Act
            var result = await _controller.GetParticipant(participant.Id);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();

            var returnedParticipant = result.Result.As<OkObjectResult>().Value.Should().BeOfType<Participant>().Subject;
            returnedParticipant.Should().BeEquivalentTo(participant, options => options
                .Including(p => p.Name)
                .Including(p => p.PreferredTea));
        }

        [Fact]
        public async Task GetParticipant_WhenParticipantDoesNotExist_ReturnsInternalServerError()
        {
            // Arrange
            var nonExistentId = 99999; // Non-existent ID

            // Act
            var result = await _controller.GetParticipant(nonExistentId);

            // Assert
            AssertInternalServerError(result.Result);
        }

        [Fact]
        public async Task CreateParticipant_WhenValidParticipant_ReturnsCreatedResult()
        {
            // Arrange
            var newParticipant = new Participant("Alice Cooper", "Oolong Tea");

            // Act
            var result = await _controller.CreateParticipant(newParticipant);

            // Assert
            // Verify the participant was actually created in the database
            var createdParticipant = await Context.Participants.FindAsync(newParticipant.Id);
            createdParticipant.Should().NotBeNull();
            createdParticipant.Should().BeEquivalentTo(newParticipant, options => options
                .Including(p => p.Name)
                .Including(p => p.PreferredTea));

            // Verify CreatedAtActionResult
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result.Result.As<CreatedAtActionResult>();
            createdResult.ActionName.Should().Be("GetParticipant");
            createdResult.RouteValues["id"].Should().Be(newParticipant.Id);

            var returnedParticipant = createdResult.Value.Should().BeOfType<Participant>().Subject;
            returnedParticipant.Should().BeEquivalentTo(newParticipant, options => options
                .Including(p => p.Name)
                .Including(p => p.PreferredTea));
        }

        [Fact]
        public async Task CreateParticipant_WhenInvalidParticipant_ReturnsInternalServerError()
        {
            // Arrange
            var invalidParticipant = new Participant("", "");

            // Act
            var result = await _controller.CreateParticipant(invalidParticipant);

            // Assert
            AssertInternalServerError(result.Result);
        }

        [Fact]
        public async Task EditParticipant_WhenValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var participant = new Participant("John Smith", "Black Tea");
            await Context.Participants.AddAsync(participant);
            await Context.SaveChangesAsync();

            participant.Name = "John Earl Smith";
            participant.PreferredTea = "Earl Grey";

            // Act
            var result = await _controller.EditParticipant(participant);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Verify the participant was actually updated in the database
            var updatedParticipant = await Context.Participants.FindAsync(participant.Id);
            updatedParticipant.Should().NotBeNull();
            updatedParticipant.Name.Should().Be(participant.Name);
            updatedParticipant.PreferredTea.Should().Be(participant.PreferredTea);
        }

        [Fact]
        public async Task EditParticipant_WhenParticipantDoesNotExist_ReturnsInternalServerError()
        {
            // Arrange
            var participant = new Participant("John Smith", "Black Tea");

            // Act
            var result = await _controller.EditParticipant(participant);

            // Assert
            AssertInternalServerError(result);
        }
    }
}