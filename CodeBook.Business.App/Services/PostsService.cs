using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Services
{
    public class PostsService : IPostService
    {
        private CodeBookContext Postdata;


        public PostsService(CodeBookContext postData)
        {
            this.Postdata = postData;
        }
        public void CreatePost(int authorId, string title,string body,bool isPublic,int? communityId,string? CodeSnippet,string? Language) 
        {
            Post post = new Post();
            post.AuthorId = authorId;
            post.Title = title;
            post.Body = body;
           // post.IsPublic = isPublic;
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
         //   post.IsPublic = isPublic;
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
            
        //    post.IsPublic = true;
            Postdata.SaveChanges();

        }

       /* public List<Post> getFeed(int postId) 
        {
            return Postdata.posts.Where(p => p.IsPublic == true).ToList();

        }
       */
         public void SavePost(int userId, int postId) 
        {
            PostSaved saved = new PostSaved();
            saved.UserId = userId;
            saved.PostId = postId;
            Postdata.postsSaved.Add(saved);
            Postdata.SaveChanges();
        }
    }
}
