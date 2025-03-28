using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.Models
{
    public class TeamParticipantSelectionEntry
    {
        public int Id { get; set; } // Primary key
        public string TeamId { get; set; }
        public List<string> Participants { get; set; }
        public string ChosenParticipant { get; set; }

        public TeamParticipantSelectionEntry(string teamId, List<string> participants, string chosenParticipant)
        {
            TeamId = teamId;
            Participants = participants;
            ChosenParticipant = chosenParticipant;
        }
    }
} 