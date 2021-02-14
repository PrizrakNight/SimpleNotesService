using SimpleNotesServer.Sdk.Models.Identification;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk.Contracts
{
    public interface INoteServiceClient
    {
        bool Authorized { get; }

        INoteClient NoteClient { get; }
        IUserProfileClient ProfileClient { get; }

        Task LoginAsync(string jwt);
        Task LoginAsync(string password, string username);
        Task RegisterAsync(RegistrationModel registrationModel);
    }
}
