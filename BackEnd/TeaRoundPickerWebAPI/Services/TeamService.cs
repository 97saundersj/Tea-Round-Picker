using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.DTOs;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeaRoundPickerContext _context;

        public TeamService(TeaRoundPickerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams
                .Include(t => t.Participants)
                .ToListAsync();
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _context.Teams
                .Include(t => t.Participants)
                .FirstAsync(t => t.Id == id);
        }

        public async Task UpdateTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                throw new ArgumentException("Team ID mismatch.");
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    throw new KeyNotFoundException("Team not found.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Team> CreateTeam(CreateTeamDto createTeamDto)
        {
            if (createTeamDto == null || string.IsNullOrWhiteSpace(createTeamDto.Label))
            {
                throw new ArgumentException("Team name is required.");
            }

            var team = new Team(0, createTeamDto.Label, []);

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
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
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
            var team = await GetTeam(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            var existingParticipent = team.Participants.FirstOrDefault(p => p.Name == participantName);
            if (existingParticipent != null)
            {
                team.Participants.Remove(existingParticipent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetRandomParticipant(int teamId)
        {
            var team = await GetTeam(teamId);
            if (team == null || !team.Participants.Any())
            {
                throw new KeyNotFoundException("Team not found or has no participants.");
            }

            var random = new Random();
            int index = random.Next(team.Participants.Count);
            var selectedParticipant = team.Participants[index];

            // Create a new ParticipantSelectionLog for each selection
            var participantSnapshots = team.Participants.Select(p => new ParticipantSnapshot
            {
                Name = p.Name,
                PreferredTea = p.PreferredTea
            }).ToList();

            var selectionEntry = new TeamParticipantSelectionEntry(
                teamId, 
                participantSnapshots, // Use snapshots
                selectedParticipant.Name
            );

            _context.TeamParticipantSelectionEntries.Add(selectionEntry);
            await _context.SaveChangesAsync();

            return selectedParticipant.Name;
        }

        public async Task<IEnumerable<TeamParticipantSelectionEntry>> GetPreviousParticipantSelectionsForTeam(int teamId)
        {
            return await _context.TeamParticipantSelectionEntries
                .Where(entry => entry.TeamId == teamId)
                .Include(t => t.Participants)
                .ToListAsync();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
} 