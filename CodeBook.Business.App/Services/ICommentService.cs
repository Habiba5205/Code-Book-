using System;
namespace CodeBook.Business.App.Services
{
    public interface ICommentService
    {
        void AddComment(string userId,string postId,string comment) { }
        void DeleteComment(string commentid) { }
        void EditComment(string commentid,string comment) { }
    }
}
