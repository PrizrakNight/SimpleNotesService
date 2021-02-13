using SimpleNotes.Server.Domain.Entities;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(SimpleUser simpleUser);
    }
}
