using Streamish.Models;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByIdWithVideo(int id);
    }
}