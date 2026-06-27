using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using AutoMapper;
using CodeBook.Data.App;
using CodeBook.Models.App;
using System;

namespace CodeBook.Business.App.Services
{
    public class PostsService : IPostService
    {
        private CodeBookContext Postdata;
        private readonly IMapper mapper;


        public PostsService(CodeBookContext postData, IMapper mapper)
        {
            this.Postdata = postData;
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
            Postdata.posts.Add(post);
            Postdata.SaveChanges();

        }
        public void UpdatePost(int postId, string title, string body, bool isPublic, int? communityId, string? CodeSnippet, string? Language) 
        {
            Post post = Postdata.posts.FirstOrDefault(p =>  p.Id == postId);
            if (post == null)
                throw new Exception("Post Not Found!!");

            post.Title = title;
            post.Body = body;
            post.IsPublic = isPublic;
            post.CommunityId = communityId;
            post.CodeSnippet = CodeSnippet;
            Postdata.SaveChanges();

        }
        public void DeletePost(int postId) 
        {
            Post post = Postdata.posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                throw new Exception("Post Not Found!!");
            
            Postdata.posts.Remove(post);
            Postdata.SaveChanges();
        }
        public void PublishPost(int postId) 
        {
            Post post = Postdata.posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                throw new Exception("Post Not Found!!");
            
           post.IsPublic = true;
            Postdata.SaveChanges();

        }

        public List<PostResponse> GetFeed(int postId) 
        {
          var feed = Postdata.posts.Where(p => p.IsPublic == true && p.IsRemoved == false).ToList();
            return mapper.Map<List<PostResponse>>(feed);

        }
   
         public void SavePost(int userId, int postId) 
        {
            PostSaved saved = new PostSaved();
            saved.UserId = userId;
            saved.PostId = postId;
            Postdata.postsSaved.Add(saved);
            Postdata.SaveChanges();
        }
        public List<PostTagDto> GetPostTags(int postId)
        {
            var tag =  Postdata.postTags.Where(p => p.PostId == postId).ToList();
            return mapper.Map<List<PostTagDto>>(tag);
        }
        public void AddTag(int postId,int tagId)
        {
            Post post = Postdata.posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                throw new Exception("Post Not Found!!");
            PostTag postTag = new PostTag();
            postTag.PostId = postId;
            postTag.TagId = tagId;
            Postdata.postTags.Add(postTag);
            Postdata.SaveChanges();
        }
        public void RemoveTag(int postId, int tagId)
        {
            Post post = Postdata.posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                throw new Exception("Post Not Found!!");
            PostTag postTag = Postdata.postTags.FirstOrDefault(p => p.PostId == postId && p.TagId == tagId);
            if (postTag == null)
                throw new Exception("Tag Not Found");

            Postdata.postTags.Remove(postTag);
            Postdata.SaveChanges();
        }

    }
}
