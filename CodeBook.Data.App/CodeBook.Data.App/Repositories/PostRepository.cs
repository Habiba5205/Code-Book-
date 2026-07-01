using CodeBook.Models.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBook.Data.App.IRepositories;

namespace CodeBook.Data.App.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly CodeBookContext _context;

        public PostRepository(CodeBookContext context) {
            _context = context;
        }

        public Post GetPostById(int postid)
        {
            Post post = _context.posts.FirstOrDefault(p => p.Id == postid);
            if (post == null) {
                throw new Exception("Post Not Found!!"); }
            else return post;
        }
        public void Add(Post post)
        {
            _context.posts.Add(post);
        }

        public void Update(Post post)
        {
            _context.posts.Update(post);
        }

        public void Delete(Post post)
        {
            _context.posts.Remove(post);
        }

        public List<Post> Getfeed()
        {
            return _context.posts.Where(p => p.IsPublic && !p.IsRemoved).ToList();
        }
        public void SavePost(PostSaved saved)
        {
            _context.postsSaved.Add(saved);
        }
        public void AddRemovalRecord(PostRemoval postRemoval)
        {
            _context.postsRemovals.Add(postRemoval);
        }
        public void AddReaction(Reaction reaction)
        {
            //_context.reactions.Add(reaction);
        }
        public void AddComment(int postId)
        {
            var post = GetPostById(postId);
            if (post != null)
            {
                post.CommentCount += 1;
            }
        }
        public List<PostTag> GetPostTags(int postId)
        {
            return _context.postTags.Where(p => p.PostId == postId).ToList();
        }
        
        public void AddTag(PostTag tag)
        {
            _context.postTags.Add(tag);
        }

        public void RemoveTag(PostTag tag)
        {
            _context.postTags.Remove(tag);
        }

        public PostTag GetPostTagbyId(int postId, int tagId)
        {
            PostTag postTag = _context.postTags.FirstOrDefault(p => p.PostId == postId && p.TagId == tagId);
            if (postTag == null)
                throw new Exception("Tag Not Found");
            return postTag;
        }
        public IQueryable<Post> GetAllUnremoved()
        {
            return _context.posts.Where(p => p.IsRemoved == false);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool IsPostSavedByUser(int userId, int postId)
        {
            return _context.postsSaved.Any(s => s.UserId == userId && s.PostId == postId);
        }

    } 
}

