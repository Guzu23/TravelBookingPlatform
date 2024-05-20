namespace DataAccessLayer.Models
{
    public class Airline : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int ChairsLeft { get; set; }

        public Airline() { }
        public Airline(string Name, string Location, int ChairsLeft)
        {
            this.Name = Name;
            this.ChairsLeft = ChairsLeft;
            this.Location = Location;
        }
    }
}
