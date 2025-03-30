using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;
using Xunit;

namespace TeaRoundPickerWebAPI.IntegrationTests
{
    public class TeamsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public TeamsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [Fact]
        public async Task CreateAndGetTeam_ShouldWorkAsExpected()
        {
            // Arrange
            var createTeamDto = new CreateTeamDto
            {
                Label = "Test Team",
            };

            // Act - Create Team
            var createResponse = await _client.PostAsync(
                "/api/Teams",
                new StringContent(
                    JsonSerializer.Serialize(createTeamDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            // Assert - Create Team
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var createdTeam = await createResponse.Content.ReadFromJsonAsync<Team>(_jsonOptions);
            Assert.NotNull(createdTeam);
            Assert.Equal(createTeamDto.Label, createdTeam.Label);

            // Act - Get Team
            var getResponse = await _client.GetAsync($"/api/Teams/{createdTeam.Id}");

            // Assert - Get Team
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var retrievedTeam = await getResponse.Content.ReadFromJsonAsync<Team>(_jsonOptions);
            Assert.NotNull(retrievedTeam);
            Assert.Equal(createdTeam.Id, retrievedTeam.Id);
            Assert.Equal(createdTeam.Label, retrievedTeam.Label);
        }

        [Fact]
        public async Task GetNonExistentTeam_ShouldReturnNotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/Teams/999999");
            response = null;
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeamWithInvalidData_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidTeamDto = new CreateTeamDto
            {
                Label = ""
            };

            // Act
            var response = await _client.PostAsync(
                "/api/Teams",
                new StringContent(
                    JsonSerializer.Serialize(invalidTeamDto),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}