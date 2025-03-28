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
        public async Task<ActionResult<Team>> GetTeam(string id)
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
        public async Task<IActionResult> PutTeam(string id, Team team)
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
        public async Task<IActionResult> DeleteTeam(string id)
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
        public async Task<IActionResult> AddParticipant(string id, [FromBody] string participantName)
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

        // DELETE: api/Teams/{id}/participants/{participantName}
        [HttpDelete("{id}/participants/{participantName}")]
        public async Task<IActionResult> RemoveParticipant(string id, string participantName)
        {
            try
            {
                await _teamService.RemoveParticipant(id, participantName);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Teams/{id}/random-participant
        [HttpGet("{id}/random-participant")]
        public async Task<ActionResult<string>> GetRandomParticipant(string id)
        {
            try
            {
                var participant = await _teamService.GetRandomParticipant(id);
                return Ok(participant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Teams/{id}/previous-participant-selections
        [HttpGet("{id}/previous-participant-selections")]
        public async Task<ActionResult<IEnumerable<TeamParticipantSelectionEntry>>> GetPreviousParticipantSelections(string id)
        {
            var selections = await _teamService.GetPreviousParticipantSelectionsForTeam(id);
            return Ok(selections);
        }
    }
}
