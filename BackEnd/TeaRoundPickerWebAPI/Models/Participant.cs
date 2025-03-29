namespace TeaRoundPickerWebAPI.Models
{
    public class Participant(int id, string name, string preferredTea)
    {
        public int Id { get; set; } = id;

        public string Name { get; set; } = name;

        public string PreferredTea { get; set; } = preferredTea;
    }
} 