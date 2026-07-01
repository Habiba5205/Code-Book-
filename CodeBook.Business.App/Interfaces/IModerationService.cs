using CodeBook.Business.App.DTOs;
using System;

namespace CodeBook.Business.App.Interfaces
{
	public interface IModerationService
    {
        void RemoveComment(int PostId, RemovePostsDto dto,int removerId);
    }
}
