using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBook.Business.App.DTOs
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CodeSnippet { get; set; }
        public string Language { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPublic { get; set; }
    }
}
