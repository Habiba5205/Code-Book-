namespace CodeBook.Models.App

{
    public abstract class BaseEntity
    {
        public int ID { set; get; };
        public DateTime DateCreated { set; get; };
        public DateTime DateUpdated { set; get; };
    }
}