using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Sample_CRUD_Application.AppService;
using Sample_CRUD_Application.DataService.BaseClasses;
using Sample_CRUD_Application.DataService.Repository;

namespace Sample_CRUD_Application.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Configure JSON serialization settings
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            builder.Services.AddScoped<BaseDataService>();
            builder.Services.AddScoped<StudentDataRepository>();
            builder.Services.AddScoped<StudentService>();


            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // data access instance and pass the configuration
            BaseDataService dataAccess = new BaseDataService(configuration);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAnyOrigin"); // Adding CORS middleware

            app.MapControllers();

            app.Run();



        }
    }
}




           