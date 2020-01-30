using Bogus;
using NUnit.Framework;
using SumApp.Shared;
using System.Threading.Tasks;

namespace SumApp.IntegrationTests
{
    [TestFixture]
    public class GlobalSetUp
    {
        public static Faker Faker = new Faker("pt_BR");
        public static TestInstance TestInstance;

        [OneTimeSetUp]
        public void OneTimeSetup() => 
            TestInstance = new TestInstanceBuilder()
            .CreateDataBase()
            .CreateBackEndServer()
            .Build();

        [TearDown]
        public async Task AllTestsTearDown() =>
           await TestInstance.ResetDatabase();

        [OneTimeTearDown]
        public void OneTimeTearDown() => TestInstance.WebHost?.Dispose();
    }
}
