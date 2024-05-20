namespace DataAccessLayer.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
