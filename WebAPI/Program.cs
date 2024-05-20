#pragma warning disable IDE0058
using DataAccessLayer.Repository;
using DataAccessLayer;
using BusinessLayer.Services.Delegates;
using BusinessLayer.Services.Linq;
using BusinessLayer.Contracts;
using BusinessLayer.Services.Logger;

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
