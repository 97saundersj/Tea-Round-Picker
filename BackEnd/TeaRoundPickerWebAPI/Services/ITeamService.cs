using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeams();

        Task<Team> GetTeam(int id);

        Task UpdateTeam(int id, Team team);

        Task<Team> CreateTeam(CreateTeamDto createTeamDto);

        Task DeleteTeam(int id);

        Task AddParticipant(int teamId, string participantName);

        Task RemoveParticipant(int teamId, string participantName);

        Task<string> GetRandomParticipant(int teamId);

        Task<IEnumerable<TeamParticipantSelectionEntry>> GetPreviousParticipantSelectionsForTeam(int teamId);
    }
} 