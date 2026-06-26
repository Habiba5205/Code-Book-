using System;

namespace CodeBook.Business.App.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        // Save something to cache (with expiry time)
        Task SetAsync<T>(string key, T value, TimeSpan expiry);
        Task RemoveAsync(string key);
        // Get the notification count for a user
        Task<int> GetNotificationBadgeCountAsync(int userId);
    }
}
