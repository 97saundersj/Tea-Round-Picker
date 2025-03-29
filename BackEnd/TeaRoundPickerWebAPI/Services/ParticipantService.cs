using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly TeaRoundPickerContext _context;

        public ParticipantService(TeaRoundPickerContext context)
        {
            _context = context;
        }

        public async Task AddParticipant(int teamId, string participantName)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            if (!team.Participants.Exists(p => p.Name == participantName))
            {
                var newParticipant = new Participant(0, participantName, "");

                team.Participants.Add(newParticipant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveParticipant(int teamId, string participantName)
        {
            var team = await _context.Teams.Include(t => t.Participants).FirstOrDefaultAsync(t => t.Id == teamId);
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
            var team = await _context.Teams.Include(t => t.Participants).FirstOrDefaultAsync(t => t.Id == teamId);
            if (team == null || !team.Participants.Any())
            {
                throw new KeyNotFoundException("Team not found or has no participants.");
            }

            var random = new Random();
            int index = random.Next(team.Participants.Count);
            var selectedParticipant = team.Participants[index];

            var selectionEntry = new TeamParticipantSelectionEntry(
                teamId, 
                team.Participants,
                selectedParticipant.Name
            );

            _context.TeamParticipantSelectionEntries.Add(selectionEntry);
            await _context.SaveChangesAsync();

            return selectedParticipant.Name;
        }
    }
} 