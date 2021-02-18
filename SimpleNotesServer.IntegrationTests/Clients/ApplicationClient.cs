using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Server.Application;
using SimpleNotes.Server.Domain.Entities;
using SimpleNotes.Server.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;

namespace SimpleNotesServer.IntegrationTests.Clients
{
    public class ApplicationClient
    {
        public HttpClient Client { get; private set; }
        public TestServer Server { get; private set; }
        public SimpleUser InstantTestUser { get; private set; }

        public ApplicationClient()
        {
            Server = new TestServer(GetConfiguredBuilder());
            Client = Server.CreateClient();

            SeedUser(Server.Host.Services.GetRequiredService<EFCoreDbContext>(), Server.Host.Services.GetRequiredService<IPasswordHasher>());
        }

        protected virtual WebHostBuilder GetConfiguredBuilder()
        {
            var builder = new WebHostBuilder();

            builder.UseStartup<Startup>();
            builder.UseEnvironment("IntegrationTests");
            builder.UseConfiguration(ConfigureConfiguration());
            builder.ConfigureServices(ConfigureServices);

            return builder;
        }

        private IConfiguration ConfigureConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.IntegrationTests.json");

            return builder.Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(EFCoreDbContext));

            if (descriptor != default)
                services.Remove(descriptor);

            services.AddDbContextPool<EFCoreDbContext>(options => options.UseInMemoryDatabase("IntegrationTestsDb"));
        }

        private void SeedUser(EFCoreDbContext eFCoreDbContext, IPasswordHasher passwordHasher)
        {
            InstantTestUser = new SimpleUser
            {
                AvatarUrl = "https://www.instantTest.com/avatar.png",
                Name = "Instant User Test",
                PasswordHash = passwordHasher.HashPassword("Instant_0rd"),
                Role = "User",
                RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds(),
                LastEntrance = DateTimeOffset.Now.ToUnixTimeSeconds()
            };

            eFCoreDbContext.Users.Add(InstantTestUser);
            eFCoreDbContext.SaveChanges();
        }
    }
}
