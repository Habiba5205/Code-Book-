using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;

namespace CodeBook.Business.App.Services
{

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void AddComment(int authorId,int postId, string CommentBody, int? selfCommentId)
        {
            Comment comment = new Comment();
            comment.AuthorId = authorId;
            comment.PostId = postId;
            comment.Body = CommentBody;
            comment.SelfCommentId = selfCommentId;
           _commentRepository.Add(comment);
           _commentRepository.SaveChanges();

        }
        public void EditComment(int commentId,string CommentBody)
        {
            Comment comment = _commentRepository.GetCommentById(commentId);
            comment.Body = CommentBody;
            _commentRepository.Update(comment);
            _commentRepository.SaveChanges();

        }
        public void DeleteComment(int commentId)
        {
            Comment comment = _commentRepository.GetCommentById(commentId);
            _commentRepository.Delete(comment);
            _commentRepository.SaveChanges();
        }

        public List<CommentDto> GetPostComments(int postId)
        {
            var comments = _commentRepository.GetByPostId(postId);

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                AuthorId = c.AuthorId,
                AuthorUsername = c.Author.UserName,
                Body = c.Body,
                LikeCount = c.LikeCount,
                SelfCommentId = c.SelfCommentId,
                DateCreated = c.DateCreated
            }).ToList();
        }
    }
}
