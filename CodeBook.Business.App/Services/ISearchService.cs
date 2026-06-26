using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Services
{
    public interface ISearchService
    {
      Task Task<List<PostResponse>> SearchPostsAsync(SearchQuery query);
    }
}
