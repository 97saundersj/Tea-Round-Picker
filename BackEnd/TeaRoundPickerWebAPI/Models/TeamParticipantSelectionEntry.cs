// Models/TeamParticipantSelectionEntry.cs
using System;
using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.Models
{
    public class TeamParticipantSelectionEntry
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public List<Participant> Participants { get; set; }
        public string ChosenParticipant { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public TeamParticipantSelectionEntry()
        {
            Participants = new List<Participant>();
        }

        public TeamParticipantSelectionEntry(int teamId, List<Participant> participants, string chosenParticipant)
        {
            TeamId = teamId;
            Participants = participants;
            ChosenParticipant = chosenParticipant;
        }
    }
}