namespace BusinessLayer.Dto
{
    public class BaseEntityDto 
    {
        public Guid Id { get; set; }

        public BaseEntityDto()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
