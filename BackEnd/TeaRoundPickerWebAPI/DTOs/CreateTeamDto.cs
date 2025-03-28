using System.Collections.Generic;

namespace TeaRoundPickerWebAPI.DTOs
{
    public class CreateTeamDto
    {
        public string Label { get; set; }
        public List<string> Participants { get; set; }
    }
} 