using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Hotel : BaseEntity
    {
        public string Name {  get; set; }
        public string Location { get; set; }
        public int RoomsLeft {  get; set; }
        
        public Hotel() { }
        public Hotel(string Name, int RoomsLeft, string Location) : base()
        {
            this.Name = Name;
            this.RoomsLeft = RoomsLeft;
            this.Location = Location;
        }
    }
}
