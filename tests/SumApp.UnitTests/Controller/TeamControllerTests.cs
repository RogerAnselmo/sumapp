using System.Net;
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
                    Repository.Save(Team)).Returns(true);

            [Test]
            public void ShouldCallRepositorySave()
            {
                Controller.Post(Team);
                A.CallTo(() => Repository.Save(Team))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public void ShouldReturnCreatedForSuccessfulPost() =>
                Controller.Post(Team)
                    .Should()
                    .Be(HttpStatusCode.Created);

            [Test]
            public void ShouldReturnInternalServerErrorForFailedPost()
            {
                A.CallTo(() => Repository.Save(Team)).Returns(false);
                Controller.Post(Team)
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
                Repository.Update(Team)).Returns(true);

            [Test]
            public void ShouldCallRepositoryUpdate()
            {
                Controller.Put(Team);
                A.CallTo(() => Repository.Update(Team))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public void ShouldReturnOkForSuccessfulPut() =>
                Controller.Put(Team)
                    .Should()
                    .Be(HttpStatusCode.OK);

            [Test]
            public void ShouldReturnInternalServerErrorForFailedPut()
            {
                A.CallTo(() => Repository.Update(Team)).Returns(false);
                Controller.Put(Team)
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
                    Repository.Remove(_id)).Returns(true);
            }

            [Test]
            public void ShouldCallRepositoryRemove()
            {
                Controller.Delete(_id);
                A.CallTo(() => Repository.Remove(_id))
                    .MustHaveHappenedOnceExactly();
            }

            [Test]
            public void ShouldReturnOkForSuccessfulDelete() =>
                Controller.Delete(_id)
                    .Should()
                    .Be(HttpStatusCode.OK);

            [Test]
            public void ShouldReturnInternalServerErrorForFailedDelete()
            {
                A.CallTo(() => Repository.Remove(A<int>._)).Returns(false);
                Controller.Delete(_id)
                    .Should()
                    .Be(HttpStatusCode.InternalServerError);
            }
        }
    }
}
