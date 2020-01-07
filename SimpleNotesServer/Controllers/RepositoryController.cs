using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleNotesServer.Data.Repositories;

namespace SimpleNotesServer.Controllers
{
    public class RepositoryController<TRepository, TContext> : ControllerBase
        where TContext : DbContext, new()
        where TRepository : BaseServerRepository<TContext>, new()
    {
        protected readonly TRepository repository = new TRepository();
    }
}