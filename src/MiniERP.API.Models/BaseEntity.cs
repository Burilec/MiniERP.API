namespace MiniERP.API.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid ApiId { get; set; } = Guid.NewGuid();
    }
}