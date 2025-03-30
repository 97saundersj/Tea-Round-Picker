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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditParticipant(int id, [FromBody] string preferredTea)
        {
            try
            {
                await _participantService.EditParticipant(id, preferredTea);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
} 