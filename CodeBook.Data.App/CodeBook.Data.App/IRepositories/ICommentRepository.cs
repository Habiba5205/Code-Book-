using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.IRepositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
        Comment GetCommentById(int commentid);
        bool SaveChanges();
    }
}
