using AutoMapper;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;

namespace CodeBook.Business.App.Services
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper mapper;


        public PostsService(IPostRepository postRepository, IMapper mapper)
        {
            this._postRepository = postRepository;
            this.mapper = mapper;
        }
        public void CreatePost(int authorId, string title,string body,bool isPublic,int? communityId,string? CodeSnippet,string? Language) 
        {
            Post post = new Post();
            post.AuthorId = authorId;
            post.Title = title;
            post.Body = body;
            post.IsPublic = isPublic;
            post.CommunityId = communityId;
            post.CodeSnippet = CodeSnippet;
            post.Language = Language;
            _postRepository.Add(post);
            _postRepository.SaveChanges();

        }
        public void UpdatePost(int postId, string title, string body, bool isPublic, int? communityId, string? CodeSnippet, string? Language) 
        {
            Post post = _postRepository.GetPostById(postId);

            post.Title = title;
            post.Body = body;
            post.IsPublic = isPublic;
            post.CommunityId = communityId;
            post.CodeSnippet = CodeSnippet;
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

    }
}
