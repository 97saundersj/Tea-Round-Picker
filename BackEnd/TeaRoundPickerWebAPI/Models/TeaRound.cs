namespace TeaRoundPickerWebAPI.Models
{
    public class TeaRound(int teamId, int chosenParticipantId, List<TeaOrder> teaOrders)
    {
        // Parameterless constructor for EF
        private TeaRound() : this(0, 0, [])
        {
        }

        public int Id { get; set; }
        public int TeamId { get; set; } = teamId;
        public int ChosenParticipantId { get; set; } = chosenParticipantId;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<TeaOrder> TeaOrders { get; set; } = teaOrders;
        
        public Team? Team { get; set; }
        public Participant? ChosenParticipant { get; set; }
    }
}