using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Server.Application.Services;
using SimpleNotes.Server.Application.Services.Implementation;

namespace SimpleNotes.Server.Application
{
    public static class DependencyInjection
    {
        public static void AddDefaultApplication(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddTransient<ITokenService, TokenService>();
        }

        public static void AddDefaultUserAccessor(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessor, UserAccessor>();
        }
    }
}
