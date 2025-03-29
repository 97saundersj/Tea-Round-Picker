using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.Models
{
    public class Team
    {
        // Parameterless constructor for EF
        public Team()
        {
            Participants = new List<Participant>(); // Initialize the list
        }

        // Constructor with parameters
        public Team(int id, string label, List<Participant> participants)
        {
            Id = id;
            Label = label;
            Participants = participants;
        }

        // Properties
        public int Id { get; set; }
        public string Label { get; set; }
        public List<Participant> Participants { get; set; }
    }
} 