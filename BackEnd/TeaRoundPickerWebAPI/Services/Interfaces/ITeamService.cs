using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeams();

        Task<Team?> GetTeam(int id);

        Task UpdateTeam(int id, Team team);

        Task<Team> CreateTeam(CreateTeamDto createTeamDto);

        Task DeleteTeam(int id);

        Task RemoveParticipant(int teamId, int participantId);

        Task<IEnumerable<TeaRound>> GetTeaRounds(int teamId);
    }
} 