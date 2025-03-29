using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Services;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        // POST: api/Participant/{teamId}
        [HttpPost("{teamId}")]
        public async Task<IActionResult> AddParticipant(int teamId, [FromBody] string participantName)
        {
            try
            {
                await _participantService.AddParticipant(teamId, participantName);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Participant/{teamId}/{participantName}
        [HttpDelete("{teamId}/{participantName}")]
        public async Task<IActionResult> RemoveParticipant(int teamId, string participantName)
        {
            try
            {
                await _participantService.RemoveParticipant(teamId, participantName);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Participant/{teamId}/random
        [HttpGet("{teamId}/random")]
        public async Task<ActionResult<string>> GetRandomParticipant(int teamId)
        {
            try
            {
                var participant = await _participantService.GetRandomParticipant(teamId);
                return Ok(participant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
} 