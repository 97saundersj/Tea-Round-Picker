using Microsoft.EntityFrameworkCore;
using TeaRoundPickerWebAPI.Data;
using TeaRoundPickerWebAPI.Models;
using TeaRoundPickerWebAPI.Services;

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
            new Team(1, "Data",
            [
                new(1, "Alice", "Normal"),
                new(2, "Bob", "Milk No Sugar"),
                new(3, "Charlie", "Milk No Sugar")
            ]),
            new Team(2, "Dev",
            [
                new(4, "David", "Green"),
                new(5, "Eve", "Black"),
                new(6, "Frank", "Herbal")
            ]),
            new Team(3, "Ops",
            [
                new(7, "George", "Oolong"),
                new(8, "Harry", "Earl Grey"),
                new(9, "Isabel", "Chai")
            ])
        );
        context.SaveChanges(); // Save changes to the in-memory database
    }
}
