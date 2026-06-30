using System;

namespace CodeBook.Business.App.DTOs
{
    public class UserProfileResponse
    {
        public int Id {  get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}