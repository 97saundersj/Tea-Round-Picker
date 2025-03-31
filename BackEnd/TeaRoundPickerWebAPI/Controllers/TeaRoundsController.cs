using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaRoundsController(ITeaRoundService teaRoundService) : ControllerBase
    {
        private readonly ITeaRoundService _teaRoundService = teaRoundService;

        [HttpGet("{teamId}")]
        public async Task<ActionResult<IEnumerable<TeaRound>>> GetTeaRounds(int teamId)
        {
            var selections = await _teaRoundService.GetTeaRounds(teamId);
            return Ok(selections);
        }

        [HttpPost("{teamId}")]
        public async Task<ActionResult<int>> AddTeaRound(int teamId)
        {
            try
            {
                var participantId = await _teaRoundService.AddTeaRound(teamId);
                return Ok(participantId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
} 