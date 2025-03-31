using TeaRoundPickerWebAPI.Models;

namespace TeaRoundPickerWebAPI.Data;

public static class DataSeeder
{
    public static void SeedData(TeaRoundPickerContext context)
    {
        if (!context.Teams.Any())
        {
            var teams = new Team[]
            {
                new("Data"),
                new("Dev"),
                new("Ops")
            };

            // Add participants to each team
            teams[0].Participants.AddRange(
            [
                new("Alice", "Normal"),
                new("Bob", "Milk No Sugar"),
                new("Charlie", "Milk 4 Sugars")
            ]);

            teams[1].Participants.AddRange(
            [
                new("David", "Green"),
                new("Eve", "Black"),
                new("Frank", "Herbal")
            ]);

            teams[2].Participants.AddRange(
            [
                new("George", "Oolong"),
                new("Harry", "Earl Grey"),
                new("Isabel", "Chai")
            ]);

            context.Teams.AddRange(teams);
            context.SaveChanges();
        }
    }
} 