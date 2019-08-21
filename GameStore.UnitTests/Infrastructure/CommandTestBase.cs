using System;
using GameStore.Persistence;

namespace GameStore.UnitTests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly GameStoreDbContext Context;

        public CommandTestBase()
        {
            Context = GameStoreContextFactory.Create();
        }

        public void Dispose()
        {
            GameStoreContextFactory.Destroy(Context);
        }
    }
}
