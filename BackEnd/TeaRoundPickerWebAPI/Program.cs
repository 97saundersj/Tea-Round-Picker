using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;
using TeaRoundPickerWebAPI.Services.Interfaces;

namespace TeaRoundPickerWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<TeaRoundPickerContext>(options => options.UseInMemoryDatabase("TeaRoundPickerDb"));

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<IParticipantService, ParticipantService>();
        builder.Services.AddScoped<ITeaRoundService, TeaRoundService>();

        // Add CORS services
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin() // Allow any origin
                       .AllowAnyMethod() // Allow any HTTP method
                       .AllowAnyHeader(); // Allow any header
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        // Use CORS policy
        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TeaRoundPickerContext>();
            DataSeeder.SeedData(context);
        }

        app.MapControllers();

        app.Run();
    }
}
