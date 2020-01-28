using Bogus;
using Microsoft.EntityFrameworkCore;
using Respawn;
using SumApp.API.Repositories.Context;
using System.Net.Http;
using System.Threading.Tasks;

namespace SumApp.IntegrationTests.Models
{
    public class TestInstance
    {
        private Checkpoint checkpoint;
        protected SumAppContext SumAppContext;
        protected HttpClient HttpClient;
        protected Faker Faker;

        protected TestInstance()
        {
            Faker = new Faker();

            checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        public async Task ResetDatabase()
        {
            if (SumAppContext == null)
                return;

            SumAppContext.Database.OpenConnection();
            await checkpoint.Reset(SumAppContext.Database.GetDbConnection());
        }

    }
}
