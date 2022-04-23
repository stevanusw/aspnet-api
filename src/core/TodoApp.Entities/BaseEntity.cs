namespace TodoApp.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public byte[]? Timestamp { get; set; }
    }
}
