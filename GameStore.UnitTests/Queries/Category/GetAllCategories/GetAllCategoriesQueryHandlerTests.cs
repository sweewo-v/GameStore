using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Queries.Category.GetAllCategories;
using GameStore.Application.ViewModels.Category;
using GameStore.Persistence;
using GameStore.UnitTests.Infrastructure;
using Shouldly;
using Xunit;

namespace GameStore.UnitTests.Queries.Category.GetAllCategories
{
    [Collection("QueryCollection")]
    public class GetAllCategoriesQueryHandlerTests
    {
        private readonly GameStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllCategoriesTest()
        {
            var handler = new GetAllCategoriesQueryHandler(_context, _mapper);
            var result = await handler.Handle(new GetAllCategoriesQuery(),
                CancellationToken.None);

            result.Count().ShouldBe(6);
            result.ShouldBeOfType<List<CategoryViewModel>>();
        }
    }
}
