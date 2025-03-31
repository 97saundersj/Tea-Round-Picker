using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface ITeaRoundService
    {
        /// <summary>
        /// Gets all tea rounds for a specific team.
        /// </summary>
        /// <param name="teamId">The team's id.</param>
        /// <returns>A collection of tea rounds for the team.</returns>
        Task<IEnumerable<TeaRound>> GetTeaRounds(int teamId);

        /// <summary>
        /// Creates a new tea round for a team.
        /// </summary>
        /// <param name="teamId">The team's id.</param>
        /// <returns>The id of the newly created tea round.</returns>
        Task<int> AddTeaRound(int teamId);
    }
} 