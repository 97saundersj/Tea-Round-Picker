using System;
using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.Models
{
    public class TeamParticipantSelectionEntry(string teamId, List<string> participants, string chosenParticipant)
    {
        public int Id { get; set; } // Primary key
        public string TeamId { get; set; } = teamId;
        public List<string> Participants { get; set; } = participants;
        public string ChosenParticipant { get; set; } = chosenParticipant;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
} 