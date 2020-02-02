using System.Net;
using System.Threading.Tasks;
using AutoBogus;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NUnit.Framework;
using SumApp.API.Controllers;
using SumApp.API.Models;
using SumApp.API.Repositories;

namespace SumApp.UnitTests.Controller
{
    public class TeamControllerTests : BaseUnitTests
    {
        protected TeamController Controller;
        protected TeamRepository Repository;
        protected Team Team;

        [SetUp]
        public void AllClassesSetUp()
        {
            Team = new Team { Name = Faker.Company.CompanyName() };
            Repository = A.Fake<TeamRepository>();
            Controller = new TeamController(Repository);
        }

        public class Post : TeamControllerTests
        {
            [SetUp]
            public void SetUp() => A.CallTo(() =>
                    Repository.SaveAsync(Team)).Returns(true);

            [Test]
            public async Task ShouldCallRepositorySave()
            {
                await Controller.Post(Team);
                A.CallTo(() => Repository.SaveAsync(Team))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public async Task ShouldReturnCreatedForSuccessfulPost() =>
                (await Controller.Post(Team))
                    .Should()
                    .Be(HttpStatusCode.Created);

            [Test]
            public async Task ShouldReturnInternalServerErrorForFailedPost()
            {
                A.CallTo(() => Repository.SaveAsync(Team)).Returns(false);
                (await Controller.Post(Team))
                    .Should()
                    .Be(HttpStatusCode.InternalServerError);
            }
        }

        public class Get : TeamControllerTests
        {
            [Test]
            public void ShouldCallGetByIdRepository()
            {
                var id = Faker.Random.Int();

                Controller.Get(id);
                A.CallTo(() => Repository.Get(id))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public void ShouldCallGetRepository()
            {
                Controller.Get();
                A.CallTo(() => Repository.Get())
                    .MustHaveHappenedOnceExactly();
            }
        }

        public class Put : TeamControllerTests
        {
            [SetUp]
            public void SetUp() => A.CallTo(() =>
                Repository.UpdateAsync(Team)).Returns(true);

            [Test]
            public async Task ShouldCallRepositoryUpdate()
            {
                await Controller.Put(Team);
                A.CallTo(() => Repository.UpdateAsync(Team))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public async Task ShouldReturnOkForSuccessfulPut() =>
                (await Controller.Put(Team))
                    .Should()
                    .Be(HttpStatusCode.OK);

            [Test]
            public async Task ShouldReturnInternalServerErrorForFailedPut()
            {
                A.CallTo(() => Repository.UpdateAsync(Team)).Returns(false);
                (await Controller.Put(Team))
                    .Should()
                    .Be(HttpStatusCode.InternalServerError);
            }
        }

        public class Delete : TeamControllerTests
        {
            private int _id;

            [SetUp]
            public void SetUp()
            {
                _id = Faker.Random.Int();
                A.CallTo(() =>
                    Repository.RemoveAsync(_id)).Returns(true);
            }

            [Test]
            public async Task ShouldCallRepositoryRemove()
            {
                await Controller.Delete(_id);
                A.CallTo(() => Repository.RemoveAsync(_id))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public async Task ShouldReturnOkForSuccessfulDelete() =>
                (await Controller.Delete(_id))
                    .Should()
                    .Be(HttpStatusCode.OK);

            [Test]
            public async Task ShouldReturnInternalServerErrorForFailedDelete()
            {
                A.CallTo(() => Repository.RemoveAsync(A<int>._)).Returns(false);
                (await Controller.Delete(_id))
                    .Should()
                    .Be(HttpStatusCode.InternalServerError);
            }
        }
    }
}
