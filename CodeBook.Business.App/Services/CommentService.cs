
﻿using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
﻿using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using Microsoft.AspNetCore.SignalR;

namespace CodeBook.Business.App.Services
{

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly INotificationService _notificationService;

        public CommentService(ICommentRepository commentRepository,INotificationService notificationService)
        {
            _commentRepository = commentRepository;
            _notificationService = notificationService;
        }

        public void AddComment(int authorId,int postId,AddCommentRequest dto)
        {
            Comment comment = new Comment();
            comment.AuthorId = authorId;
            comment.PostId = postId;
            comment.Body = dto.Body;
            comment.SelfCommentId = dto.SelfCommentId;
           _commentRepository.Add(comment);
           _commentRepository.SaveChanges();

            _notificationService.CreateNotification(authorId, new NotificationDTO
            {
                UserId = authorId,
                Type = "Comment",
                Message = "You have a new Comment on your post",
                ReferenceId = postId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });

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
