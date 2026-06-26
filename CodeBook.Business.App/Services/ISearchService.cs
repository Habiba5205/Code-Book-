using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Services
{
    public interface ISearchService
    {
      List<PostResponse> SearchPosts(SearchQuery query);
    }
}
