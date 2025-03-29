// Models/TeamParticipantSelectionEntry.cs
using System;
using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.Models
{
    public class TeaRound
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<TeaOrder> TeaOrders { get; set; }
        public string ChosenParticipant { get; set; }
        

        public TeaRound()
        {
            TeaOrders = new List<TeaOrder>();
        }

        public TeaRound(int teamId, List<TeaOrder> teaOrders, string chosenParticipant)
        {
            TeamId = teamId;
            TeaOrders = teaOrders;
            ChosenParticipant = chosenParticipant;
        }
    }
}