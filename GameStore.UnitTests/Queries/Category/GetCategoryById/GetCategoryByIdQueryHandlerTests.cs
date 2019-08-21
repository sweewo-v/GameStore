using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Queries.Category.GetCategoryById;
using GameStore.Application.ViewModels.Category;
using GameStore.Persistence;
using GameStore.UnitTests.Infrastructure;
using Shouldly;
using Xunit;

namespace GameStore.UnitTests.Queries.Category.GetCategoryById
{
    [Collection("QueryCollection")]
    public class GetCategoryByIdQueryHandlerTests
    {
        private readonly GameStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCategoryByExistedIdTest()
        {
            var handler = new GetCategoryByIdQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetCategoryByIdQuery { Id = 1 },
                CancellationToken.None);

            result.Id.ShouldBe(1);
            result.Name.ShouldBe("RPG");

            result.ShouldBeOfType<CategoryViewModel>();
        }

        [Fact]
        public async Task GetCategoryByNonExistedIdTest()
        {
            var handler = new GetCategoryByIdQueryHandler(_context, _mapper);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new GetCategoryByIdQuery { Id = 100 },
                    CancellationToken.None)
            );
        }
    }
}
