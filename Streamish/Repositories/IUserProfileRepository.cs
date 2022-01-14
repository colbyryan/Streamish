using Streamish.Models;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        public UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetByIdWithVideo(int id);
        public void Add(UserProfile userProfile);
    }
}