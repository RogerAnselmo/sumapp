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
    public class TeamControllerTests : GlobalSetUp
    {
        private const string controllerUrl = "Team";
        private Team team;

        [SetUp]
        public async Task SetUp()
        {
            team = new Team { Name = Faker.Person.FirstName };
            TestInstance.SumAppContext.Add(team);
            await TestInstance.SumAppContext.SaveChangesAsync();
        }

        [Test]
        public async Task ShouldGet()
        {
            var result = await TestInstance.HttpClient.GetAsync(controllerUrl);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldGetById()
        {
            var getUrl = string.Format("{0}/{1}", controllerUrl, team.Id);
            var result = await TestInstance.HttpClient.GetAsync(getUrl);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldPost()
        {
            //TODO: chamar o builder de team
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new Team { }), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await TestInstance.HttpClient.PostAsync(controllerUrl, httpContent);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldPut()
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await TestInstance.HttpClient.PutAsync(controllerUrl, httpContent);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldDelete()
        {
            var deleteUrl = string.Format("{0}/{1}", controllerUrl, team.Id);

            var result = await TestInstance.HttpClient.DeleteAsync(deleteUrl);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
