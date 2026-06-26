using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using CodeBook.Data.App;

namespace CodeBook.Business.App.Interfaces
{
    public interface ISearchService
    {
      List<PostResponse> SearchPosts(SearchQuery query);
    }
}
