using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SumApp.API;
using SumApp.API.Repositories.Context;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace SumApp.Shared
{
    public class TestInstanceBuilder:TestInstance
    {
        public IConfiguration Configuration;

        public TestInstanceBuilder()
        {
            Configuration = GetConfiguration();
            SumAppConnectionString = Configuration.GetSection("ConnectionStrings:SumAppConnStr").Value;
        }

        public TestInstanceBuilder CreateBackEndServer()
        {
            var builder = new HostBuilder().ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer()
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseEnvironment("Test");
            });

            WebHost = builder.Start();
            HttpClient = WebHost.GetTestServer().CreateClient();

            return this;
        }

        private IConfiguration GetConfiguration()
        {
            var backServerPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "..", "src", "SumApp.API");

            return new ConfigurationBuilder()
                      .AddJsonFile("appsettings.Test.json", optional: false)
                      .SetBasePath(backServerPath)
                      .Build();
        }

        public TestInstanceBuilder CreateDataBase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<SumAppContext>();
            var sqlOptionsBuilder = new SqlServerDbContextOptionsBuilder(dbOptionsBuilder);

            sqlOptionsBuilder.MigrationsAssembly(typeof(SumAppContext).GetTypeInfo().Assembly.GetName().Name);
            dbOptionsBuilder.UseSqlServer(SumAppConnectionString);

            SumAppContext = new SumAppContext(dbOptionsBuilder.Options);
            SumAppContext.Database.EnsureDeleted();
            SumAppContext.Database.Migrate();

            return this;
        }

        public TestInstance Build() => this;
    }
}
