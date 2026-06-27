using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CodeBookContext _context;
        public CommentRepository(CodeBookContext context) { _context = context; }
        public Comment GetCommentById(int commentid)
        {
            Comment comment = _context.comments.FirstOrDefault(c => c.Id == commentid);
            if (comment == null)
                throw new Exception("Comment Not Found!!");
            return comment;
        }
        public void Add(Comment comment)
        {
            _context.comments.Add(comment);
        }
        public void Update(Comment comment)
        {
            _context.comments.Update(comment);
        }
        public void Delete(Comment comment)
        {
            _context.comments.Remove(comment);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }


    }
}
