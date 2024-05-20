#pragma warning disable IDE0058
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer
{
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString = @"Server=.\SQLExpress04;Database=TravelBookingPlatform;Trusted_Connection=True;TrustServerCertificate=true";

        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<HotelReservation> HotelReservations { get; set; }
        public DbSet<FlightReservation> FlightReservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_windowsConnectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                ;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>();
            builder.Entity<Hotel>();
            builder.Entity<Airline>();
            builder.Entity<HotelReservation>();
            builder.Entity<FlightReservation>();
        }
    }
}
