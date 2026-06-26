using System;
using CodeBook.Business.App.Services;
using CodeBook.Data.App;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Methods
{

    public class CommentService : ICommentService
    {
        private CodeBookContext commentdata;

        public CommentService(CodeBookContext Commentdata)
        {
            commentdata = Commentdata;
        }

        public void Add(int authorId,int postId, string CommentBody)
        {
            Comment comment = new Comment();
            comment.AuthorId = authorId;
            comment.PostId = postId;
            comment.Body = CommentBody;
            commentdata.Comments.Add(comment);
            commentdata.SaveChanges();

        }
        public void Edit(int commentId,string CommentBody)
        {
            Comment comment = commentdata.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new Exception("Comment Not Found!!");
            comment.Body = CommentBody;
            commentdata.SaveChanges();

        }
        public void Delete(int commentId)
        {
            Comment comment = commentdata.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new Exception("Comment Not Found!!");
            commentdata.Comments.Remove(comment);
            commentdata.SaveChanges();
        }
    }
}
