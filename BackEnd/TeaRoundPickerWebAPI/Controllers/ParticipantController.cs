using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController(IParticipantService participantService) : ControllerBase
    {
        private readonly IParticipantService _participantService = participantService;

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