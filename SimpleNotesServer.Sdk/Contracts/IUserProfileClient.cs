using SimpleNotesServer.Sdk.Models.User;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk.Contracts
{
    public interface IUserProfileClient
    {
        DetailedUserModel CurrentProfile { get; }

        Task<byte[]> DownloadAvatarAsync();

        Task<DetailedUserModel> GetProfileAsync();
        Task<DetailedUserModel> UpdateProfileAsync(UserModel userModel);
    }
}
