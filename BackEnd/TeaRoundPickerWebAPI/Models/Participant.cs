namespace TeaRoundPickerWebAPI.Models
{
    public class Participant(string name, string preferredTea)
    {
        // Parameterless constructor for EF
        private Participant() : this(string.Empty, string.Empty)
        {
        }

        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; } = name;
        public string PreferredTea { get; set; } = preferredTea;

        public Team? Team { get; set; }
    }
} 