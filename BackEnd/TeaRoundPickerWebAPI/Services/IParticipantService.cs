using System.Threading.Tasks;

namespace TeaRoundPickerWebAPI.Services
{
    public interface IParticipantService
    {
        Task AddParticipant(int teamId, string participantName);

        Task RemoveParticipant(int teamId, string participantName);

        Task<string> GetRandomParticipant(int teamId);

        Task EditParticipant(int teamId, string oldParticipantName, string newParticipantName);
    }
} 