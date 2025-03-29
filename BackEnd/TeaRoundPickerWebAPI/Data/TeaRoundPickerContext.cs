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

        public DbSet<Participant> Participants { get; set; }

        public DbSet<TeaRound> TeaRounds { get; set; }

        public DbSet<TeaOrder> TeaOrders { get; set; }
    }
}