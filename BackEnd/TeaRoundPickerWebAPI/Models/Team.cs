namespace TeaRoundPickerWebAPI.Models
{
    public class Team(string label)
    {
        // Parameterless constructor for EF
        private Team() : this(string.Empty)
        {
        }

        // Properties
        public int Id { get; set; }
        public string Label { get; set; } = label;
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}