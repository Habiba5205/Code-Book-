using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Business.App.DTOs
{
    public class AddCommentRequest
    {
        public int AuthorId { get; set; }
        public string Body { get; set; }
        public int? SelfCommentId { get; set; }
    }
}
