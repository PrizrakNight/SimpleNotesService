using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleNotes.Server.Application;
using SimpleNotes.Server.Application.Options;
using SimpleNotes.Server.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
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

            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddSingleton(options);
            services.AddDefaultApplication();
            services.AddDefaultApplicationFilters();
            services.AddHttpContextAccessor();
            services.AddDefaultInfrastructure(conf => conf.UseInMemoryDatabase("Test database"));

            services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddSwaggerGen(ConfigureSwaggerGen);

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

        private void ConfigureSwaggerGen(SwaggerGenOptions obj)
        {
            obj.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            });

            obj.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
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

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Notes v1.0");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
