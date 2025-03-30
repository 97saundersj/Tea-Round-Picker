using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services
{
    public class ParticipantService(TeaRoundPickerContext context, ITeamService teamService) : IParticipantService
    {
        private readonly TeaRoundPickerContext _context = context;
        private readonly ITeamService _teamService = teamService;

        public async Task<Participant> GetParticipant(int id)
        {
            return await _context.Participants
                .FirstAsync(t => t.Id == id);
        }

        public async Task AddParticipant(int teamId, string participantName)
        {
            var team = await _teamService.GetTeam(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            if (!team.Participants.Exists(p => p.Name == participantName))
            {
                var newParticipant = new Participant(participantName, "");
                _context.Participants.Add(newParticipant);

                team.Participants.Add(newParticipant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveParticipant(int teamId, string participantName)
        {
            var team = await _teamService.GetTeam(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            var existingParticipant = team.Participants.FirstOrDefault(p => p.Name == participantName);
            if (existingParticipant != null)
            {
                team.Participants.Remove(existingParticipant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetRandomParticipant(int teamId)
        {
            var team = await _teamService.GetTeam(teamId);
            if (team == null || !team.Participants.Any())
            {
                throw new KeyNotFoundException("Team not found or has no participants.");
            }

            var random = new Random();
            int index = random.Next(team.Participants.Count);
            var selectedParticipant = team.Participants[index];

            var teaOrders = team.Participants
                .Select(p => new TeaOrder(p.Id, p.PreferredTea))
                .ToList();

            var teaRound = new TeaRound(
                teamId,
                selectedParticipant.Id,
                teaOrders
            );

            _context.TeaRounds.Add(teaRound);
            await _context.SaveChangesAsync();

            return selectedParticipant.Name;
        }

        public async Task EditParticipant(int id, string preferredTea)
        {
            var participant = await GetParticipant(id);

            participant.PreferredTea = preferredTea;

            await _context.SaveChangesAsync();
        }
    }
}