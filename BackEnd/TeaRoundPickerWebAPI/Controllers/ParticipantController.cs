using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController(IParticipantService participantService) : ControllerBase
    {
        private readonly IParticipantService _participantService = participantService;

        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            var participant = await _participantService.GetParticipant(id);
            if (participant == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error getting Participant.");

            return Ok(participant);
        }

        [HttpPost]
        public async Task<ActionResult<Participant>> CreateParticipant([FromBody] Participant participant)
        {
            try
            {
                await _participantService.CreateParticipant(participant);
                return CreatedAtAction("GetParticipant", new { id = participant.Id }, participant);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Participant.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditParticipant([FromBody] Participant participant)
        {
            try
            {
                await _participantService.EditParticipant(participant);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error editing Participant.");
            }
        }
    }
}