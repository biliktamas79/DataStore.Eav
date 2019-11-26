using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core.Tests.Unit
{
    [TestClass]
    public class ReflectionEntityDefinitionProviderUnitTests
    {
        private static readonly ReflectionEntityDefinitionProvider Instance = new ReflectionEntityDefinitionProvider(new ReflectionEntityDefinitionKeyProvider());

        [TestMethod]
        public async Task GetEntityDefinitionOrThrow_TestPocoClassWithAllSupportedPropertyTypes()
        {
            var entityDefinition = await Instance.GetEntityDefinitionOrThrow<TestPocoClassWithAllSupportedPropertyTypes>(CancellationToken.None)
                .ConfigureAwait(false);

            Assert.IsNotNull(entityDefinition);
            Assert.IsFalse(string.IsNullOrWhiteSpace(entityDefinition.Key));
        }
    }
}
