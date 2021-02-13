using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SimpleNotes.Server.Application;
using SimpleNotes.Server.Application.Options;
using SimpleNotes.Server.Infrastructure;
using System.Text;

namespace SimpleNotesServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var options = Configuration.GetSection("TokenProviderOptions").Get<TokenProviderOptions>();
            var securityKey = GetSecurityKey(options);

            services.AddSingleton(options);
            services.AddDefaultApplication();
            services.AddDefaultApplicationFilters();
            services.AddDefaultUserAccessor();
            services.AddHttpContextAccessor();
            services.AddDefaultInfrastructure(conf => conf.UseInMemoryDatabase("Test database"));

            services.AddControllers();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,

                        ValidateAudience = true,
                        ValidAudience = options.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = securityKey,
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        private SymmetricSecurityKey GetSecurityKey(TokenProviderOptions options)
        {
            var appSecretBytes = Encoding.ASCII.GetBytes(options.ApplicationSecret);

            return new SymmetricSecurityKey(appSecretBytes);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
