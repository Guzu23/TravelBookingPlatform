namespace WebAPI.Dto
{
    public class AirlineDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int ChairsLeft { get; set; }

        public AirlineDto() { }
        public AirlineDto(string Name, string Location, int ChairsLeft) : base()
        {
            this.Name = Name;
            this.Location = Location;
            this.ChairsLeft = ChairsLeft;
        }
    }
}
