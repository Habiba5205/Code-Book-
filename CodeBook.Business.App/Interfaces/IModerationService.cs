using CodeBook.Business.App.DTOs;
using System;

namespace CodeBook.Business.App.Interfaces
{
	public interface IModerationService
    {
        void RemovePost(int PostId, RemovePostsDto dto,int removerId);
    }
}
