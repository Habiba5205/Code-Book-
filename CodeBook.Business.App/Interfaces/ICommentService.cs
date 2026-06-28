using CodeBook.Business.App.DTOs;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface ICommentService
    {
        void AddComment(int authorId, int postId, string CommentBody, int? salfComment);
        void DeleteComment(int commentId);
        void EditComment(int commentId, string CommentBody);
        List<CommentDto> GetPostComments(int postId);
    }
}
