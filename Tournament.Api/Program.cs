using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Api.AutoMapper;
using Tournament.Api.Extensions;
using Tournament.Core.Repositories;
using Tournament.Data.Data;
using Tournament.Data.Repositories;

namespace Tournament.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentApiContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TournamentApiContext")
                        ?? throw new InvalidOperationException(
                            "Connection string 'TournamentApiContext' not found."
                        )
                )
            );

            // Add services to the container.
            // 1.0 hj�lper oss att mappa mot Json och Xml, patch
            builder
                .Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //AutoMapping
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            //Repo service
            //builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.ConfigureCors();

            var app = builder.Build();

            // Seed the database
            try
            {
                await app.SeedDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
                throw; // Optionally rethrow or log more details
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
