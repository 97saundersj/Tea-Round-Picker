using System.Threading.Tasks;

namespace TeaRoundPickerWebAPI.Services.Interfaces
{
    public interface IParticipantService
    {
        Task AddParticipant(int teamId, string participantName);

        Task<string> GetRandomParticipant(int teamId);

        Task EditParticipant(int id, string preferredTea);
    }
} 