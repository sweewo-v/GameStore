using MediatR;

namespace GameStore.Application.Commands.Game.DeleteGame
{
    public class DeleteGameCommand : IRequest
    {
        public int Id { get; set; }
    }
}
