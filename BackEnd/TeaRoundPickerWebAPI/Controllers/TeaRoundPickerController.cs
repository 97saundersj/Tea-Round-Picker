using Microsoft.AspNetCore.Mvc;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TeaRoundPickerController(ILogger<TeaRoundPickerController> logger) : ControllerBase
{
    private static readonly Team[] Teams = [
       new Team("Team A", "Team A", ["Alice", "Bob", "Charlie"]),
       new Team("Team B", "Team B",["David", "Eve", "Frank"]),
       new Team("Team C", "Team C",["George", "Harry", "Isabel"])
    ];

    private readonly ILogger<TeaRoundPickerController> _logger = logger;

    [HttpGet("teams")]
    public IEnumerable<Team> GetTeams()
    {
        return Teams;
    }
}
