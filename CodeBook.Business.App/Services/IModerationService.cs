using System;

namespace CodeBook.Business.App.Services
{
	public interface IModerationService
    {
		void RemovePost(int removerid, int postId, int reportId, string reason);
	}
}
