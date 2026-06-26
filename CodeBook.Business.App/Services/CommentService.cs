using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Services
{

    public class CommentService : ICommentService
    {
        private CodeBookContext commentdata;

        public CommentService(CodeBookContext Commentdata)
        {
            commentdata = Commentdata;
        }

        public void AddComment(int authorId,int postId, string CommentBody)
        {
            Comment comment = new Comment();
            comment.AuthorId = authorId;
            comment.PostId = postId;
            comment.Body = CommentBody;
            commentdata.comments.Add(comment);
            commentdata.SaveChanges();

        }
        public void EditComment(int commentId,string CommentBody)
        {
            Comment comment = commentdata.comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                throw new Exception("Comment Not Found!!");
            comment.Body = CommentBody;
            commentdata.SaveChanges();

        }
        public void DeleteComment(int commentId)
        {
            Comment comment = commentdata.comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                throw new Exception("Comment Not Found!!");
            commentdata.comments.Remove(comment);
            commentdata.SaveChanges();
        }
    }
}
