using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

using ELearning.API;
using ELearning.Infrastructure.Data;
using ELearning.API.DTOs.Auth;
using ELearning.Core.Domain;

namespace ELearning.Tests
{
    public class AuthControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _output;

        public AuthControllerIntegrationTests(
            WebApplicationFactory<Program> factory,
            ITestOutputHelper output)
        {
            _output = output;
            var jwtSettings = new Dictionary<string, string?>
            {
                ["JwtSettings:SecretKey"] = "a‑32‑char‑long‑secret‑for‑tests!!!",
                ["JwtSettings:Issuer"] = "TestIssuer",
                ["JwtSettings:Audience"] = "TestAudience",
                ["JwtSettings:ExpiryInDays"] = "1"
            };

            _factory = factory.WithWebHostBuilder(builder =>
            {
                // 1) Override configuration for both AuthService & JwtBearer
                builder.ConfigureAppConfiguration((ctx, cfg) =>
                    cfg.AddInMemoryCollection(jwtSettings));

                // 2) Swap in-memory EF Core for ApplicationDbContext
                builder.ConfigureServices(services =>
                {
                    var desc = services
                        .Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    services.Remove(desc);
                    services.AddDbContext<ApplicationDbContext>(opts =>
                        opts.UseInMemoryDatabase("TestDb"));
                });
            });
        }

        [Fact]
        public async Task Register_Login_Me_EndToEnd()
        {
            var client = _factory.CreateClient();

            // 1) Register
            var regPayload = JsonSerializer.Serialize(new
            {
                Email = "carol@example.com",
                Password = "Pass123!",
                FirstName = "Carol",
                LastName = "Clark",
                Role = (int)UserRole.Student
            });
            var regResp = await client.PostAsync(
                "/api/auth/register",
                new StringContent(regPayload, Encoding.UTF8, "application/json")
            );
            Assert.Equal(HttpStatusCode.OK, regResp.StatusCode);

            // 2) Login
            var loginPayload = JsonSerializer.Serialize(new
            {
                Email = "carol@example.com",
                Password = "Pass123!"
            });
            var loginResp = await client.PostAsync(
                "/api/auth/login",
                new StringContent(loginPayload, Encoding.UTF8, "application/json")
            );
            Assert.Equal(HttpStatusCode.OK, loginResp.StatusCode);

            var loginContent = await loginResp.Content.ReadAsStringAsync();
            _output.WriteLine($"Login response ({(int)loginResp.StatusCode}): {loginContent}");

            // Deserialize case‑insensitive
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var loginResult = JsonSerializer.Deserialize<LoginResponseDto>(loginContent, opts);
            Assert.NotNull(loginResult);
            Assert.False(string.IsNullOrEmpty(loginResult.Token));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResult.Token);

            // 3) GET /api/auth/me
            var meResp = await client.GetAsync("/api/auth/me");
            Assert.Equal(HttpStatusCode.OK, meResp.StatusCode);
        }
    }
}
