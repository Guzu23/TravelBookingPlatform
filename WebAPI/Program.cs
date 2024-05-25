#pragma warning disable IDE0058
using DataAccessLayer.Repository;
using DataAccessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Services
            builder.Services.AddScoped<IUserDelegate, UserDelegate>();
            builder.Services.AddScoped<ILinqHotelReservation, LinqHotelReservation>();
            builder.Services.AddScoped<BusinessLayer.Contracts.ILogger, Logger>();
            builder.Services.AddScoped<BusinessLayer.Services.AirlinesService>();
            builder.Services.AddScoped<BusinessLayer.Services.FlightReservationsService>();
            builder.Services.AddScoped<BusinessLayer.Services.HotelReservationsService>();
            builder.Services.AddScoped<BusinessLayer.Services.HotelsService>();
            builder.Services.AddScoped<BusinessLayer.Services.UsersService>();

            //Database
            builder.Services.AddDbContext<MyDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
