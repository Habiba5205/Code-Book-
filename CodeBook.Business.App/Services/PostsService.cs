using AutoMapper;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeBook.Business.App.Services
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper mapper;
        private readonly CodeBookContext _context;


        public PostsService(IPostRepository postRepository, IMapper mapper, CodeBookContext context)
        {
            this._postRepository = postRepository;
            this.mapper = mapper;
            _context = context;
        }
        public void CreatePost(CreatePostRequest request) 
        {
            Post post = new Post
            {
                AuthorId = request.AuthorId,
                Title = request.Title,
                Body = request.Body,
                IsPublic = request.IsPublic,
                CommunityId = request.CommunityId,
                CodeSnippet = request.CodeSnippet,
                Language = request.Language
            };

            _postRepository.Add(post);
            _postRepository.SaveChanges();

            if(request.TagIds != null && request.TagIds.Any())
            {
                foreach(var tagId in request.TagIds)
                {
                    bool tagExists = _context.tags.Any(t => t.Id == tagId);
                    if (tagExists)
                    {
                        PostTag postTag = new PostTag
                        {
                            PostId = post.Id,
                            TagId = tagId
                        };
                        _postRepository.AddTag(postTag);
                    }
                
                }
                _postRepository.SaveChanges();
            }
        }
        public void UpdatePost(int postId, UpdatePostRequest request) 
        {
            Post post = _postRepository.GetPostById(postId);

            post.Title = request.Title;
            post.Body = request.Body;
            post.IsPublic = request.IsPublic;
            post.CommunityId = request.CommunityId;
            post.CodeSnippet = request.CodeSnippet;
            post.Language = request.Language;
            post.DateUpdated = DateTime.UtcNow;
            _postRepository.SaveChanges();

        }
        public void DeletePost(int postId) 
        {
            Post post = _postRepository.GetPostById(postId);

            _postRepository.Delete(post);
            _postRepository.SaveChanges();
        }
        public void PublishPost(int postId) 
        {
            Post post = _postRepository.GetPostById(postId);

            post.IsPublic = true;
            _postRepository.SaveChanges();

        }

        public List<PostResponse> GetFeed(int postId) 
        {

            var feed = _postRepository.Getfeed();
            return mapper.Map<List<PostResponse>>(feed);

        }
   
         public void SavePost(int userId, int postId) 
        {
            PostSaved saved = new PostSaved();
            saved.UserId = userId;
            saved.PostId = postId;
            _postRepository.SavePost(saved);
            _postRepository.SaveChanges();
        }
        public List<PostTagDto> GetPostTags(int postId)
        {
            var tag = _postRepository.GetPostTags(postId);
            return mapper.Map<List<PostTagDto>>(tag);
        }
        public void AddTag(int postId,int tagId)
        {
            Post post = _postRepository.GetPostById(postId);
            PostTag postTag = new PostTag();
            postTag.PostId = postId;
            postTag.TagId = tagId;
            _postRepository.AddTag(postTag);
            _postRepository.SaveChanges();
        }
        public void RemoveTag(int postId, int tagId)
        {
            Post post = _postRepository.GetPostById(postId);
            PostTag postTag = _postRepository.GetPostTagbyId(postId, tagId);

            _postRepository.RemoveTag(postTag);
            _postRepository.SaveChanges();
        }
        public Post GetPost(int postId) { 
                return  _postRepository.GetPostById(postId);
        }
        public int GetPostAuthorId(int postId)
        {
            var post = _postRepository.GetPostById(postId);
            return post.AuthorId;
        }

    }
}
