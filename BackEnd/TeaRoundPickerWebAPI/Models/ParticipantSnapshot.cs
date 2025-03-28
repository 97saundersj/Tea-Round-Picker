// Models/ParticipantSnapshot.cs
namespace TeaRoundPickerWebAPI.Models
{
    public class ParticipantSnapshot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreferredTea { get; set; }

        public int TeamParticipantSelectionEntryId { get; set; }
    }
}