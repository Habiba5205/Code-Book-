using System;
namespace CodeBook.Business.App.Services
{
    public interface IPostService
    {
        void CreatePost(string userId,string post) { }
        void UpdatePost(string postId,string post) { }
        void DeletePost(string postId) { }
        void PublishPost(string postId) { }
        void ArchivePost(string postId)  { }
        void getFeed(string postId) { }
        void SavePost(string userid,string postId) { }


    }
}
