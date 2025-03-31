using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Services
{
    public class TeamService(TeaRoundPickerContext context) : ITeamService
    {
        private readonly TeaRoundPickerContext _context = context;

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams
                .ToListAsync();
        }

        public async Task<Team?> GetTeam(int id)
        {
            return await _context.Teams
                .Include(t => t.Participants)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> CreateTeam(Team team)
        {
            if (team == null || string.IsNullOrWhiteSpace(team.Label))
            {
                throw new ArgumentException("Team name is required.");
            }

            _context.Teams.Add(team);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeamExists(team.Id))
                {
                    throw new InvalidOperationException("Team already exists.");
                }
                else
                {
                    throw;
                }
            }

            return team;
        }

        public async Task DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new KeyNotFoundException("Team not found.");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task AddParticipant(int id, int participantId)
        {
            var team = await GetTeam(id);
            if (team == null) throw new KeyNotFoundException("Team not found.");

            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.Id == participantId);

            if (participant == null) throw new KeyNotFoundException("Participant not found.");

            var participantAlreadyAdded = team.Participants.Exists(p => p.Id == participantId);
            if (participantAlreadyAdded) throw new InvalidOperationException("Participant Already Added.");

            team.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveParticipant(int teamId, int participantId)
        {
            var team = await GetTeam(teamId);
            if (team == null) throw new KeyNotFoundException("Team not found.");

            var participant = team.Participants.FirstOrDefault(p => p.Id == participantId);
            if (participant != null)
            {
                team.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}