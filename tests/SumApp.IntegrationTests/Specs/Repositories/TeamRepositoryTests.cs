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
        public void Setup() => teamRepository = new TeamRepository(SumAppContext);


        [Test]
        public void ShouldSave()
        {
            teamRepository.Save(new Team { Name = Faker.Person.FirstName });
            SumAppContext.Teams.Count().Should().Be(1);
        }
    }
}
