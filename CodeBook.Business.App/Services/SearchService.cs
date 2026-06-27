using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using AutoMapper;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;

namespace CodeBook.Business.App.Services
{
    public class SearchService : ISearchService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper mapper;
        public SearchService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            this.mapper = mapper;
        }

        public List<PostResponse> SearchPosts(SearchQuery query)
        {
            var posts = _postRepository.GetAllUnremoved();

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
           return mapper.Map<List<PostResponse>>(posts);
            /* return posts.Select(p => new PostResponse
             {
                 Id = p.Id,
                 Title = p.Title,
                 Body = p.Body,
                 CodeSnippet = p.CodeSnippet,
                 Language = p.Language,
                 AuthorUsername = p.Author.UserName,
                 DateCreated = p.DateCreated
             }).ToList();*/
        }
    }
}
