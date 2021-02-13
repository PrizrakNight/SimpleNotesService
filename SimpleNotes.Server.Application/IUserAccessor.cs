using SimpleNotes.Server.Domain.Entities;

namespace SimpleNotes.Server.Application
{
    public interface IUserAccessor
    {
        SimpleUser CurrentUser { get; }

        int CurrentUserId { get; }
    }
}
