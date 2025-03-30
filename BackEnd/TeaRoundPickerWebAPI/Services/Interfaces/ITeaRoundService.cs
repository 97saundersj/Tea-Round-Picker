using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface ITeaRoundService
    {
        Task<IEnumerable<TeaRound>> GetTeaRounds(int teamId);
        Task<string> AddTeaRound(int teamId);
    }
} 