using System;

namespace CodeBook.Business.App.Services
{
	public interface IModerationService
    {
		Task RemovePostAsync(int removerid, int postId, int reportId, string reason);
	}
}
