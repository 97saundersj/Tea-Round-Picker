using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Data
{
    public class TeaRoundPickerContext : DbContext
    {
        public TeaRoundPickerContext(DbContextOptions<TeaRoundPickerContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamParticipantSelectionEntry> TeamParticipantSelectionEntries { get; set; }
    }
}