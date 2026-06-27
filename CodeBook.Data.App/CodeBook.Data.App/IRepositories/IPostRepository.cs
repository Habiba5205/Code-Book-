using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.IRepositories
{
    public interface IPostRepository
    {
        Post GetPostById(int postid);
        void Add(Post post);
        void Update(Post post);
        void Delete(Post post);
        IQueryable<Post> GetAllUnremoved();

        List<Post> Getfeed();
        void SavePost(PostSaved saved);
        void AddRemovalRecord(PostRemoval postRemoval);
        List<PostTag> GetPostTags(int postId);
        void AddTag(PostTag tag);
        void RemoveTag(PostTag tag);
        PostTag GetPostTagbyId(int postId, int tagId);
        bool SaveChanges();


    }

}
