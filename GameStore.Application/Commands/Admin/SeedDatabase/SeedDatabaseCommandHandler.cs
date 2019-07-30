using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Interfaces;
using MediatR;

namespace GameStore.Application.Commands.Admin.SeedDatabase
{
    public class SeedDatabaseCommandHandler : IRequestHandler<SeedDatabaseCommand>
    {
        private readonly IGameStoreDbContext dbContext;

        public SeedDatabaseCommandHandler(IGameStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Unit> Handle(SeedDatabaseCommand request, CancellationToken cancellationToken)
        {
            if (!dbContext.Games.Any() || !dbContext.Categories.Any())
            {
                dbContext.SeedData();
            }

            return Unit.Task;
        }
    }
}
