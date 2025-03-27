namespace TeaRoundPickerWebAPI.Models
{
    public class Team(string id, string label, List<string> participants)
    {
        public string Id { get; set; } = id;
        public string Label { get; set; } = label;
        public List<string> Participants { get; set; } = participants;
    }
} 