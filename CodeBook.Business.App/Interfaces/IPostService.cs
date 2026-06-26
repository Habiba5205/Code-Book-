using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IPostService
    {
        void CreatePost(int authorId, string title, string body, bool isPublic, int? communityId, string? CodeSnippet, string? Language);
        void UpdatePost(int postId, string title, string body, bool isPublic, int? communityId, string? CodeSnippet, string? Language);
        void DeletePost(int postId);
        void PublishPost(int postId);
      //  int getFeed(int postId);
        void SavePost(int userid, int postId);


    }
}
