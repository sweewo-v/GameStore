using GameStore.Application.Queries.Game.GetAllGames;
using GameStore.Application.ViewModels.Game;
using MediatR;

namespace GameStore.Application.Queries.Game.GetGameById
{
    public class GetGameByIdQuery : IRequest<GameViewModel>
    {
        public int Id { get; set; }
    }

}
