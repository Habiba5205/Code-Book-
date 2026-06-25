namespace CodeBook.Services
{
    public interface ICommunityService
    {
        void CreateCommunity(string userId,string name) { }
        void JoinCommunity(string userId,string communityId) { }
        void AssignRole(string communityId, string userId, string role) { }
        void UpdateCommunity(string userId,string communityId) { }

    }
}
