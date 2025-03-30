using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.DTOs;
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
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(CreateTeamDto createTeamDto)
        {
            try
            {
                var team = await _teamService.CreateTeam(createTeamDto);
                return CreatedAtAction("GetTeam", new { id = team.Id }, team);
            }
            catch (ArgumentException)
            {
                return BadRequest("Team name is required.");
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }

        [HttpDelete("{teamId}/{participantId}")]
        public async Task<IActionResult> RemoveParticipant(int teamId, int participantId)
        {
            try
            {
                await _teamService.RemoveParticipant(teamId, participantId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
