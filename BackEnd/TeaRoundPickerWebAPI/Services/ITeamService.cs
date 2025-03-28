using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeams();

        Task<Team> GetTeam(string id);

        Task UpdateTeam(string id, Team team);

        Task<Team> CreateTeam(CreateTeamDto createTeamDto);

        Task DeleteTeam(string id);

        Task AddParticipant(string teamId, string participantName);

        Task RemoveParticipant(string teamId, string participantName);

        Task<string> GetRandomParticipant(string teamId);

        Task<IEnumerable<TeamParticipantSelectionEntry>> GetPreviousParticipantSelectionsForTeam(string teamId);
    }
} 