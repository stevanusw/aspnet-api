namespace TodoApp.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public DateTime LastUpdateDateUtc { get; set; }
        public byte[]? Timestamp { get; set; }
    }
}
