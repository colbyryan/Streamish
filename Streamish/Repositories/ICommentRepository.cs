using Streamish.Models;

namespace Streamish.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
    }
}