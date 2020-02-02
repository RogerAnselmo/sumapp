using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Respawn;
using SumApp.API.Repositories.Context;
using System.Net.Http;
using System.Threading.Tasks;

namespace SumApp.Shared
{
    public class TestInstance
    {
        public IHost WebHost;
        public Checkpoint checkpoint;
        public SumAppContext SumAppContext;
        public HttpClient HttpClient;
        public string SumAppConnectionString { get; protected set; }

        protected TestInstance() => checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" }
        };

        public async Task ResetDatabase()
        {
            if (SumAppContext != null)
            {
                await SumAppContext.Database.OpenConnectionAsync();
                await checkpoint.Reset(SumAppContext.Database.GetDbConnection());
                await SumAppContext.Database.CloseConnectionAsync();
            }
        }
    }
}
