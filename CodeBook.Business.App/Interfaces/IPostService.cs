using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IPostService
    {
        void CreatePost(int userId,CreatePostRequest request);
        void UpdatePost(int postId, UpdatePostRequest request,int userId);
        void DeletePost(int postId, int userId);
        void PublishPost(int postId);
        List<PostResponse> GetFeed(int postId);
        void SavePost(int userid, int postId);
        List<PostTagDto> GetPostTags(int postId);
        void AddTag(int postId, int tagId);
        void RemoveTag(int postId, int tagId);
        Post GetPost(int postId);
        int GetPostAuthorId(int postId);



    }
}
