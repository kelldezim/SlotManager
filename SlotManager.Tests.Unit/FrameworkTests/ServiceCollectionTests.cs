using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SlotManager.Tests.Unit.FrameworkTests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void test()
        {
            var serviceCollection = new ServiceCollection();



            var serviceProvider = serviceCollection.BuildServiceProvider();
        }

    }
}
