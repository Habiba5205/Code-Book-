using System;

namespace CodeBook.Business.App.DTOs
{
    public class UserProfileResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }
    }
}