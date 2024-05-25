namespace BusinessLayer.Dto
{
    public class HotelDto : BaseEntityDto
    {
        public string Name {  get; set; }
        public string Location { get; set; }
        public int RoomsLeft {  get; set; }

        public HotelDto() { }
        public HotelDto(string Name, int RoomsLeft, string Location) : base()
        {
            this.Name = Name;
            this.RoomsLeft = RoomsLeft;
            this.Location = Location;
        }
    }
}
