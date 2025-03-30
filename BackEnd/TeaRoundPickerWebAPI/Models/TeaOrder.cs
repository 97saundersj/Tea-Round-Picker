namespace TeaRoundPickerWebAPI.Models
{
    public class TeaOrder(int participantId, string requestedTeaOrder)
    {
        // Parameterless constructor for EF
        private TeaOrder() : this(0, string.Empty)
        {
        }

        public int Id { get; set; }
        public int TeaRoundId { get; set; }
        public int ParticipantId { get; set; } = participantId;
        public string RequestedTeaOrder { get; set; } = requestedTeaOrder;

        public TeaRound? TeaRound { get; set; }
        public Participant? Participant { get; set; }
    }
}