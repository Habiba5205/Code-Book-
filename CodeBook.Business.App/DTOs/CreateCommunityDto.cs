namespace CodeBook.Business.App.DTOs
{
    public class CreateCommunityDto
    {
        public int OwnerId{ get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }=string.Empty;
    }
}
