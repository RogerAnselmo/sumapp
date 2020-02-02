using Bogus;
using FluentAssertions;
using NUnit.Framework;
using SumApp.API.Models;
using SumApp.API.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace SumApp.IntegrationTests.Specs.Repositories
{
    public class TeamRepositoryTests: GlobalSetUp
    {
        private TeamRepository teamRepository;

        [SetUp]
        public void Setup() => teamRepository = new TeamRepository(TestInstance.SumAppContext);

        [Test]
        public async Task ShouldSave()
        {
            await teamRepository.SaveAsync(new Team { Name = Faker.Person.FirstName });
            TestInstance.SumAppContext.Teams.Count().Should().Be(1);
        }

        [Test]
        public async Task ShouldGet()
        {
            await InsertTeam();
            await InsertTeam();
            await InsertTeam();

            (await teamRepository.Get()).Count().Should().Be(3);
        }

        [Test]
        public async Task ShouldGetById()
        {
            await InsertTeam();
            await InsertTeam();
            await InsertTeam();

            var team = await InsertTeam();

            (await teamRepository.Get(team.Id)).Should().BeEquivalentTo(team);
        }


        [Test]
        public async Task ShouldUpdate()
        {
            var team = await InsertTeam();

            team.Name = "updated name";

            await teamRepository.UpdateAsync(team);

            TestInstance
                .SumAppContext
                .Teams
                .Find(team.Id)
                .Should().BeEquivalentTo(team);
        }

        [Test]
        public async Task ShouldDelete()
        {
            var team = await InsertTeam();

            await teamRepository.RemoveAsync(team.Id);

            TestInstance
                .SumAppContext
                .Teams
                .Find(team.Id)
                .Should().BeNull();
        }

        public async Task<Team> InsertTeam(Team team = null)
        {
            team ??= new Team { Name = Faker.Person.FirstName };
            TestInstance.SumAppContext.Teams.Add(team);
            await TestInstance.SumAppContext.SaveChangesAsync();

            return team;
        }
    }
}
