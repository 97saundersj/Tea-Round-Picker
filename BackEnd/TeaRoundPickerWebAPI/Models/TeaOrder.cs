namespace TeaRoundPickerWebAPI.Models
{
    public class TeaOrder(int participantId, string participantName, string preferredTeaOrder)
    {
        // Parameterless constructor for EF
        private TeaOrder() : this(0, string.Empty, string.Empty)
        {
        }

        // Properties
        public int Id { get; set; }
        public int ParticipantId { get; set; } = participantId;
        public string ParticipantName { get; set; } = participantName;
        public string PreferredTeaOrder { get; set; } = preferredTeaOrder;

        public TeaRound? TeaRound { get; set; }
    }
}