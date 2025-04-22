using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

using ELearning.Core.Domain;
using ELearning.Core.Common;
using ELearning.Infrastructure.Data;
using ELearning.API.Services;
using ELearning.API.DTOs.Auth;

namespace ELearning.Tests
{
    public class AuthServiceInMemoryTests
    {
        private readonly IConfiguration _config;

        public AuthServiceInMemoryTests()
        {
            // in‑memory JWT settings
            var settings = new Dictionary<string, string?>
            {
                ["JwtSettings:SecretKey"] = "a‑32‑char‑long‑secret‑key‑for‑tests!",
                ["JwtSettings:Issuer"] = "TestIssuer",
                ["JwtSettings:Audience"] = "TestAudience",
                ["JwtSettings:ExpiryInDays"] = "1"
            };
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }

        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Register_Then_Login_Works()
        {
            // Arrange: fresh in‑memory DB + real repo + service
            await using var db = CreateContext();
            var repo = new Repository<User>(db);
            var svc = new AuthService(repo, _config);

            var registerDto = new RegisterDto
            {
                Email = "alice@example.com",
                Password = "Secret123!",
                FirstName = "Alice",
                LastName = "Anderson",
                Role = UserRole.Student
            };

            // Act: register
            var regResult = await svc.RegisterAsync(registerDto);

            // Assert: registration returned a token + correct user
            Assert.NotNull(regResult.Token);
            Assert.Equal("alice@example.com", regResult.User.Email);

            // Act: login with the same credentials
            var loginDto = new LoginDto
            {
                Email = "alice@example.com",
                Password = "Secret123!"
            };
            var loginResult = await svc.LoginAsync(loginDto);

            // Assert: login also returns token + same user
            Assert.NotNull(loginResult.Token);
            Assert.Equal(regResult.User.Id, loginResult.User.Id);
        }

        [Fact]
        public async Task Login_InvalidCredentials_Throws()
        {
            await using var db = CreateContext();
            var repo = new Repository<User>(db);
            var svc = new AuthService(repo, _config);

            // no users in DB yet → login should fail
            var loginDto = new LoginDto
            {
                Email = "bob@example.com",
                Password = "Wrong!"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => svc.LoginAsync(loginDto));
        }
    }
}
