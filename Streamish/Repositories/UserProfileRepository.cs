using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System.Collections.Generic;
using System.Linq;


namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public UserProfile GetByIdWithVideo(int id)
        {
            UserProfile profile = null;
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        select up.*,
                        v.Id AS VideoId,
                        v.Title,
                        v.Description,
                        v.Url,
                        v.DateCreated VideoDateCreated,
                        c.Id CommentId,
                        c.Message,
                        c.UserProfileId CommentUserProfileId
                        from UserProfile up
                        LEFT JOIN Video v ON v.UserProfileId = up.Id
                        LEFT JOIN Comment c ON c.VideoId = v.Id
                        Where up.Id = 1
                                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (profile == null)
                            {
                                profile = new UserProfile
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                    Videos = new List<Video>()
                                };
                            }

                        }
                        if (DbUtils.IsNotDbNull(reader, "VideoId"))
                        {
                            var existingVideo = profile.Videos.FirstOrDefault(v => v.Id == DbUtils.GetInt(reader, "VideoId"));
                            if (existingVideo == null)
                            {
                                existingVideo = new Video
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
                                    Url = DbUtils.GetString(reader, "url"),
                                    Comments = new List<Comment>()
                                };
                                profile.Videos.Add(existingVideo);
                            }
                            if (DbUtils.IsNotDbNull(reader, "CommentId"))
                            {
                                existingVideo.Commets.Add(new Comment
                                {
                                    Id = DbUtils.GetInt(reader, "CommentId"),
                                    Message = DbUtils.GetString(reader, "Message"),
                                    UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
                                });
                            }
                        }
                    }
                }
            }
            return profile;
        }
    }
}
