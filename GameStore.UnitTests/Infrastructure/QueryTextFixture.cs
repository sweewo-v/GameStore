using AutoMapper;
using System;
using GameStore.Persistence;
using Xunit;

namespace GameStore.UnitTests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public GameStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = GameStoreContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            GameStoreContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
