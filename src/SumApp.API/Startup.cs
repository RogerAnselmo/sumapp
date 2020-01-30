using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SumApp.API.Repositories;
using SumApp.API.Repositories.Context;

namespace SumApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", 
                    new OpenApiInfo
                    {
                        Title = "Projeto Api", 
                        Version = "1.0"
                    }));

            var connectionString = Configuration.GetSection("ConnectionStrings:SumAppConnStr");
            services.AddDbContext<SumAppContext>(x => x.UseSqlServer(connectionString.Value));
            //services.AddDbContext<SumAppContext>(x => x.UseSqlServer("Server=tcp:mssql-container,1433;Initial Catalog=SumDBTest;User ID=SA; Password=ABC!@#321a"));

            services.AddScoped<TeamRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SumApp");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
