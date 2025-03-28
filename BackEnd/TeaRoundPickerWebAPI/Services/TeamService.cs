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
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeam(string id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task UpdateTeam(string id, Team team)
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

            var team = new Team(Guid.NewGuid().ToString(), createTeamDto.Label, createTeamDto.Participants ?? new List<string>());

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

        public async Task DeleteTeam(string id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task AddParticipant(string teamId, string participantName)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            if (!team.Participants.Contains(participantName))
            {
                team.Participants.Add(participantName);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveParticipant(string teamId, string participantName)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            if (team.Participants.Contains(participantName))
            {
                team.Participants.Remove(participantName);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetRandomParticipant(string teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null || !team.Participants.Any())
            {
                throw new KeyNotFoundException("Team not found or has no participants.");
            }

            var random = new Random();
            int index = random.Next(team.Participants.Count);
            string selectedParticipant = team.Participants[index];

            // Create a new TeamParticipantSelectionEntry for each selection
            var selectionEntry = new TeamParticipantSelectionEntry(teamId, team.Participants, selectedParticipant);

            _context.TeamParticipantSelectionEntries.Add(selectionEntry);
            await _context.SaveChangesAsync();

            return selectedParticipant;
        }

        public async Task<IEnumerable<TeamParticipantSelectionEntry>> GetPreviousParticipantSelectionsForTeam(string teamId)
        {
            return await _context.TeamParticipantSelectionEntries
                .Where(entry => entry.TeamId == teamId)
                .ToListAsync();
        }

        private bool TeamExists(string id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
} 