using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface ITeamService
    {
        /// <summary>
        /// Gets all teams.
        /// </summary>
        /// <returns>A collection of all teams.</returns>
        Task<IEnumerable<Team>> GetTeams();

        /// <summary>
        /// Gets a team by their id.
        /// </summary>
        /// <param name="id">The team's id.</param>
        /// <returns>The team if found; otherwise, null.</returns>
        Task<Team?> GetTeam(int id);

        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="team">The team details.</param>
        /// <returns>The newly created team.</returns>
        Task<Team> CreateTeam(Team team);

        /// <summary>
        /// Deletes a team by their id.
        /// </summary>
        /// <param name="id">The team's id.</param>
        Task DeleteTeam(int id);

        /// <summary>
        /// Removes a participant from a team.
        /// </summary>
        /// <param name="teamId">The team's id.</param>
        /// <param name="participantId">The participant's id.</param>
        Task RemoveParticipant(int teamId, int participantId);

        /// <summary>
        /// Adds a participant to a team.
        /// </summary>
        /// <param name="id">The team's id.</param>
        /// <param name="participantId">The participant's id.</param>
        Task AddParticipant(int id, int participantId);
    }
}