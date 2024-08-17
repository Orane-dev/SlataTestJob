
using Microsoft.EntityFrameworkCore;
using SlataTestJob.BL.Implementation;
using SlataTestJob.BL.Interfaces;
using SlataTestJob.DAL.Implementation;
using SlataTestJob.DAL.Interfaces;
using SlataTestJob.DAL.Models;

namespace SlataTestJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString(
                    Environment.GetEnvironmentVariable("Connection_string")));
            });
            builder.Services.AddTransient<ICandidateDAL, CandidateDAL>();
            builder.Services.AddTransient<ICandidateBL, CandidateBL>();
            builder.Services.AddTransient<IEmployeeDAL, EmployeeDAL>();
            builder.Services.AddTransient<IEmployeeBL, EmployeeBL>();
            builder.Services.AddTransient<ITestJobBL, TestJobBL>();
            builder.Services.AddTransient<ITestJobDAL, TestJobDAL>();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80);
            });

            var app = builder.Build();

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
