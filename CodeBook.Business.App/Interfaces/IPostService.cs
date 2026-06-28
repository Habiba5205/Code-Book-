using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IPostService
    {
        void CreatePost(CreatePostRequest request);
        void UpdatePost(int postId, UpdatePostRequest request);
        void DeletePost(int postId);
        void PublishPost(int postId);
        List<PostResponse> GetFeed(int postId);
        void SavePost(int userid, int postId);
        List<PostTagDto> GetPostTags(int postId);
        void AddTag(int postId, int tagId);
        void RemoveTag(int postId, int tagId);



    }
}
