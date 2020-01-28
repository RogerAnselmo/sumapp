using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using SumApp.API.Repositories.Context;
using System;
using System.IO;
using System.Reflection;

namespace SumApp.IntegrationTests.Models
{
    public class TestInstanceBuilder: TestInstance
    {
        public IConfiguration Configuration;

        public TestInstanceBuilder() => Configuration = GetConfiguration();

        private IConfiguration GetConfiguration()
        {
            var backServerPath = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "..", "src", "SumApp.API");

            return new ConfigurationBuilder()
                      .AddJsonFile("appsettings.Test.json", optional: false)
                      .SetBasePath(backServerPath)
                      .Build();
        }

        protected void CreateDataBase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<SumAppContext>();
            var sqlOptionsBuilder = new SqlServerDbContextOptionsBuilder(dbOptionsBuilder);

            sqlOptionsBuilder.MigrationsAssembly(typeof(SumAppContext).GetTypeInfo().Assembly.GetName().Name);
            dbOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("SumAppConnStr"));

            SumAppContext = new SumAppContext(dbOptionsBuilder.Options);
            SumAppContext.Database.EnsureDeleted();
            SumAppContext.Database.Migrate();
        }
    }
}
