using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using SumApp.API.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SumApp.IntegrationTests.Specs.Controllers
{
    public class TeamControllerTests: GlobalSetUp
    {
        private const string controllerUrl = "Team";
        private Team team;

        [Test]
        public async Task ShouldGetTeams()
        {
            var result = await HttpClient.GetAsync(controllerUrl);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldPostTeams()
        {
            team = new Team { Name = Faker.Person.FirstName};

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await HttpClient.PostAsync(controllerUrl, httpContent);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
