using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TeaRoundPickerContext>(options => options.UseInMemoryDatabase("TeaRoundPickerDb"));

builder.Services.AddControllers();

builder.Services.AddScoped<ITeamService, TeamService>();

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
    SeedData(context);
}

app.MapControllers();

app.Run();

static void SeedData(TeaRoundPickerContext context)
{
    if (!context.Teams.Any()) // Check if data already exists
    {
        context.Teams.AddRange(
            new Team("teamA", "Team A", ["Alice", "Bob", "Charlie"]),
            new Team("teamB", "Team B", ["David", "Eve", "Frank"]),
            new Team("teamC", "Team C", ["George", "Harry", "Isabel"])
        );
        context.SaveChanges(); // Save changes to the in-memory database
    }
}
