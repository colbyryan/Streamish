using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streamish.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (Message, VideoId, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@Message, @VideoId, @UserProfileId)";

                    DbUtils.AddParameter(cmd, "@Title", comment.Message);
                    DbUtils.AddParameter(cmd, "@Description", comment.VideoId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", comment.UserProfileId);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
