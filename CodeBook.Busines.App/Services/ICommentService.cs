using System;
namespace CodeBook.Business.App.Services
{
    public interface ICommentService
    {
        void AddComment(int authorId, int postId, string CommentBody);
        void DeleteComment(int commentId);
        void EditComment(int commentId, string CommentBody);
    }
}
