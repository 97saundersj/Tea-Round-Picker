using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService teamService) : ControllerBase
    {
        private readonly ITeamService _teamService = teamService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return Ok(await _teamService.GetTeams());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamService.GetTeam(id);
            if (team == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error getting Team.");

            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            try
            {
                var createdTeam = await _teamService.CreateTeam(team);
                return CreatedAtAction("GetTeam", new { id = createdTeam.Id }, createdTeam);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Team.");
            }
        }

        [HttpPost("{teamId}/participants/{participantId}")]
        public async Task<IActionResult> AddParticipant(int teamId, int participantId)
        {
            try
            {
                await _teamService.AddParticipant(teamId, participantId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding Participant.");
            }

            return NoContent();
        }

        [HttpDelete("{teamId}/{participantId}")]
        public async Task<IActionResult> RemoveParticipant(int teamId, int participantId)
        {
            try
            {
                await _teamService.RemoveParticipant(teamId, participantId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error removing Participant.");
            }
        }
    }
}
