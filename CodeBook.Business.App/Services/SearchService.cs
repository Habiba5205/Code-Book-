using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using System;

namespace CodeBook.Business.App.Services
{
    public class SearchService : ISearchService
    {
        private readonly CodeBookContext context;
        public SearchService(CodeBookContext context)
        {
            this.context = context;
        }

        public List<PostResponse> SearchPosts(SearchQuery query)
        {
            var posts = context.posts.Where(p => p.IsRemoved == false);

            if(!string.IsNullOrEmpty(query.Keyword))
            {
                posts = posts.Where(p => p.Title.Contains(query.Keyword) || p.Body.Contains(query.Keyword));
            }
            if (!string.IsNullOrEmpty(query.Language))
            {
                posts = posts.Where(p => p.Language == query.Language);
            }
            if(query.CommunityId != null)
            {
                posts = posts.Where(p => p.CommunityId == query.CommunityId);
            }
            if (!string.IsNullOrEmpty(query.Tag))
                posts = posts.Where(p => p.PostTags
                             .Any(t => t.Tag.Name == query.Tag));
            return posts.Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                CodeSnippet = p.CodeSnippet,
                Language = p.Language,
                AuthorUsername = p.Author.UserName,
                DateCreated = p.DateCreated
            }).ToList();
        }
    }
}
