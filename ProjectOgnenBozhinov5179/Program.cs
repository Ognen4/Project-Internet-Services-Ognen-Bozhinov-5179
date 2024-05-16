

using ProjectOgnenBozhinov5179.Data;
using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Repositories;
using ProjectOgnenBozhinov5179.Repositories.Implementation;
using ProjectOgnenBozhinov5179.Services;
using ProjectOgnenBozhinov5179.Services.Implementation;

namespace ProjectOgnenBozhinov5179
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?? throw new InvalidOperationException("Database couldn't be found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


            //Repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductRepository>();

            //Services
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IProductsService, ProductService>();

            builder.Services.AddControllers();
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


            app.MapControllers();

            app.Run();
        }
    }
}
