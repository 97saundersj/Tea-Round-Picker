using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI.Services
{
    public class TeaRoundService(TeaRoundPickerContext context, ITeamService teamService) : ITeaRoundService
    {
        private readonly TeaRoundPickerContext _context = context;
        private readonly ITeamService _teamService = teamService;

        public async Task<IEnumerable<TeaRound>> GetTeaRounds(int teamId)
        {
            return await _context.TeaRounds
                .Where(entry => entry.TeamId == teamId)
                .Include(t => t.TeaOrders).ThenInclude(t => t.Participant)
                .Include(t => t.ChosenParticipant)
                .ToListAsync();
        }

        public async Task<string> AddTeaRound(int teamId)
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
    }
} 