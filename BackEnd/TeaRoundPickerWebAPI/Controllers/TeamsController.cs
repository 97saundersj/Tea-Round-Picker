using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService teamService) : ControllerBase
    {
        private readonly ITeamService _teamService = teamService;

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return Ok(await _teamService.GetTeams());
        }

        // GET: api/Teams/5
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

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            try
            {
                await _teamService.UpdateTeam(id, team);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(CreateTeamDto createTeamDto)
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

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                await _teamService.DeleteTeam(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Teams/{id}/participants
        [HttpPost("{id}/participants")]
        public async Task<IActionResult> AddParticipant(int id, [FromBody] string participantName)
        {
            try
            {
                await _teamService.AddParticipant(id, participantName);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Teams/{teamId}/participants/{participantName}
        [HttpDelete("{teamId}/participants/{participantName}")]
        public async Task<IActionResult> RemoveParticipant(int teamId, string participantName)
        {
            try
            {
                await _teamService.RemoveParticipant(teamId, participantName);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Teams/{teamId}/random-participant
        [HttpGet("{teamId}/random-participant")]
        public async Task<ActionResult<string>> GetRandomParticipant(int teamId)
        {
            try
            {
                var participant = await _teamService.GetRandomParticipant(teamId);
                return Ok(participant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Teams/{teamId}/previous-participant-selections
        [HttpGet("{teamId}/previous-participant-selections")]
        public async Task<ActionResult<IEnumerable<TeamParticipantSelectionEntry>>> GetPreviousParticipantSelections(int teamId)
        {
            var selections = await _teamService.GetPreviousParticipantSelectionsForTeam(teamId);
            return Ok(selections);
        }
    }
}
