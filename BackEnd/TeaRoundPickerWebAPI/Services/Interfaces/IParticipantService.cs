using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface IParticipantService
    {
        /// <summary>
        /// Gets a participant by their id.
        /// </summary>
        /// <param name="id">The participant's id.</param>
        /// <returns>The participant if found; otherwise, null.</returns>
        Task<Participant?> GetParticipant(int id);

        /// <summary>
        /// Creates a new participant.
        /// </summary>
        /// <param name="participant">The participant details.</param>
        Task CreateParticipant(Participant participant);

        /// <summary>
        /// Updates an existing participant's details.
        /// </summary>
        /// <param name="participant">The updated participant details.</param>
        Task EditParticipant(Participant participant);
    }
}