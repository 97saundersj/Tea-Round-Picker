using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Services
{
    public class ParticipantService(TeaRoundPickerContext context) : IParticipantService
    {
        private readonly TeaRoundPickerContext _context = context;

        public async Task<Participant?> GetParticipant(int id)
        {
            return await _context.Participants
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateParticipant(Participant participant)
        {
            if (participant == null || string.IsNullOrWhiteSpace(participant.Name))
            {
                throw new ArgumentException("Participant name is required.");
            }

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task EditParticipant(Participant participant)
        {
            var existingParticipant = await GetParticipant(participant.Id);

            if (existingParticipant == null) throw new KeyNotFoundException("Participant not found.");

            existingParticipant.Name = participant.Name;
            existingParticipant.PreferredTea = existingParticipant.PreferredTea;

            await _context.SaveChangesAsync();
        }
    }
}