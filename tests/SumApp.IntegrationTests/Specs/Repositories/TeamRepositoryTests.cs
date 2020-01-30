using FluentAssertions;
using NUnit.Framework;
using SumApp.API.Models;
using SumApp.API.Repositories;
using System.Linq;

namespace SumApp.IntegrationTests.Specs.Repositories
{
    public class TeamRepositoryTests: GlobalSetUp
    {
        private TeamRepository teamRepository;

        [SetUp]
        public void Setup() => teamRepository = new TeamRepository(TestInstance.SumAppContext);


        [Test]
        public void ShouldSave()
        {
            teamRepository.Save(new Team { Name = Faker.Person.FirstName });
            TestInstance.SumAppContext.Teams.Count().Should().Be(1);
        }


        [Test]
        public void ShouldGet()
        {
            teamRepository.Save(new Team { Name = Faker.Person.FirstName });
            teamRepository.Save(new Team { Name = Faker.Person.FirstName });
            teamRepository.Save(new Team { Name = Faker.Person.FirstName });
            TestInstance.SumAppContext.Teams.Count().Should().Be(3);
        }
    }
}
