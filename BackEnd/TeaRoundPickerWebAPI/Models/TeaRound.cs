namespace TeaRoundPickerWebAPI.Models
{
    public class TeaRound(int teamId, List<TeaOrder> teaOrders, string chosenParticipant)
    {
        // Parameterless constructor for EF
        private TeaRound() : this(0, [], string.Empty)
        {
        }

        // Properties
        public int Id { get; set; }
        public int TeamId { get; set; } = teamId;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<TeaOrder> TeaOrders { get; set; } = teaOrders;
        public string ChosenParticipant { get; set; } = chosenParticipant;

        public Team? Team { get; set; }
    }
}