using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;

namespace NZWalks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // add our own created DBContext services to the container
            // we have injected our DBContext class into Services collection
            builder.Services.AddDbContext<NZWalksDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
            });

            // Inject the interface and Repository implementation
            builder.Services.AddScoped<IRegionRepository, RegionRepository>();

            //inject the Profiles for Automapper 
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

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