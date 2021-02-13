using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Server.Domain.Contracts;
using System;

namespace SimpleNotes.Server.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddDefaultInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> configureContext)
        {
            services.AddTransient<IRepositoryWrapper, EntityFrameworkCoreRepositoryWrapper>();

            services.AddDbContextPool<EntityFrameworkDbContext>(configureContext);
        }
    }
}
