using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using SumApp.API;
using SumApp.IntegrationTests.Models;
using System.Threading.Tasks;

namespace SumApp.IntegrationTests
{
    [TestFixture]
    public class GlobalSetUp: TestInstanceBuilder
    {
        private IHost _host;

        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            CreateDataBase();

            var builder = new HostBuilder().ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer()
                    .UseStartup<Startup>()
                    .UseConfiguration(Configuration)
                    .UseEnvironment("Test");
                });

            _host = await builder.StartAsync();
            HttpClient = _host.GetTestServer()
                .CreateClient();
        }

        [TearDown]
        public async Task AllTestsTearDown() =>
           await ResetDatabase();

        [OneTimeTearDown]
        public void OneTimeTearDown() => _host?.Dispose();
    }
}
