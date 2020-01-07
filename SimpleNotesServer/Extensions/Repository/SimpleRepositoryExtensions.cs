using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;
using SimpleNotesServer.Extensions.Entity;

namespace SimpleNotesServer.Extensions.Repository
{
    public static class SimpleRepositoryExtensions
    {
        public static bool AnyUser(this SimpleServerRepository repository, Func<SimpleUser, bool> predicate) =>
            repository.Context.Users.Any(predicate);

        public static SimpleUser GetUserByClaimsPrincipal(this SimpleServerRepository repository,
            ClaimsPrincipal principal) => repository.GetUserBy(user => user.Name == principal.Identity.Name);

        public static SimpleUser GetUserByNameAndPassword(this SimpleServerRepository repository, string username,
            string password) =>
            repository.GetUserBy(user => user.Name == username && user.ComparePassword(password));

        public static SimpleUser GetUserFromRequest(this SimpleServerRepository repository, HttpRequest request) =>
            repository.GetUserByNameAndPassword(request.Form["username"], request.Form["password"]);
    }
}