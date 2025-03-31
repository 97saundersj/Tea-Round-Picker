using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeaRoundPickerWebAPI.Data;
using Xunit;

namespace TeaRoundPickerWebAPI.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        protected readonly TeaRoundPickerContext Context;

        protected TestBase()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<TeaRoundPickerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            Context = new TeaRoundPickerContext(options);
        }

        public async Task InitializeAsync()
        {
            await Context.Database.EnsureCreatedAsync();
            await Context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await Context.SaveChangesAsync();
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }

        protected static void AssertInternalServerError(IActionResult? result)
        {
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
} 