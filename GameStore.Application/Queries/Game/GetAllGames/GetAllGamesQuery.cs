using GameStore.Application.ViewModels.Game;
using GameStore.Application.ViewModels.Pagination;
using MediatR;

namespace GameStore.Application.Queries.Game.GetAllGames
{
    public class GetAllGamesQuery : IRequest<PageViewModel<GameViewModel>>
    {
        public PageRequestViewModel Pagination { get; set; }

        public string Category { get; set; }
    }
}
