using Bogus;

namespace SumApp.UnitTests
{
    public class BaseUnitTests
    {
        protected Faker Faker;

        protected BaseUnitTests() => Faker = new Faker();
    }
}
